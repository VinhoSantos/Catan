using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Catan.CodeGenerator.TsGenerator
{
    public class TsGenerator
    {
        public static string Generate(TsGeneratorSettings settings)
        {
            var modules = new Dictionary<string, TsModule>();
            var types = settings.GetTypes();

            foreach (var type in types)
            {
                MapType(settings, modules, type);
            }

            var result = new StringBuilder();
            //result.Append("import { Guid } from './guid';\r\n");
            settings.WriteModules(result, modules.Values);
            return result.ToString();
        }

        private static void MapType(TsGeneratorSettings settings, Dictionary<string, TsModule> modules, Type type)
        {
            var moduleName = settings.GetModuleName(type.Namespace);
            if (!modules.ContainsKey(moduleName))
                modules.Add(moduleName, new TsModule(moduleName));

            var tsModule = modules[moduleName];
            var typeName = settings.GetTypeName(type);
            if (tsModule.Types.Any(x => x.Name == typeName))
                return;

            var baseType = type.IsInterface ? null : type.BaseType;
            var baseTypeString = settings.GetBaseType(baseType);

            var interfaces = type.IsEnum ? new Type[0] : type.GetInterfaces().Where(x => !x.Name.Contains("`")).ToArray();
            var interfaceStrings = interfaces.Select(settings.GetBaseType).ToList();

            var tsType = new TsType(type.IsEnum ? "enum" : (type.IsInterface ? "interface" : "class"), typeName, baseTypeString, interfaceStrings, tsModule);

            if (baseTypeString != null)
                MapType(settings, modules, baseType);

            foreach (var @interface in interfaces)
            {
                MapType(settings, modules, @interface);
            }

            tsModule.Types.Add(tsType);

            foreach (var propertyInfo in type.GetProperties())
            {
                if (!settings.MapProperty(type, propertyInfo))
                    continue;

                var propertyName = settings.GetPropertyName(propertyInfo);
                var propertyType = settings.GetPropertyType(propertyInfo);
                tsType.Properties.Add(new TsProperty(propertyName, propertyType, tsType, null, false));

                foreach (var referencedType in settings.GetReferencedTypes(propertyInfo.PropertyType))
                {
                    MapType(settings, modules, referencedType);
                }
            }

            if (type.IsEnum)
            {
                foreach (var fieldInfo in type.GetFields(BindingFlags.Public | BindingFlags.Static))
                {
                    if (!settings.MapField(fieldInfo))
                        continue;

                    var fieldName = settings.GetEnumName(fieldInfo);
                    var fieldValue = fieldInfo.GetRawConstantValue();
                    tsType.Properties.Add(new TsProperty(fieldName, null, tsType, fieldValue, false));
                }
            }
            else
            {
                foreach (var fieldInfo in type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance))
                {
                    if (!settings.MapField(fieldInfo))
                        continue;

                    var fieldName = settings.GetFieldName(fieldInfo);
                    var fieldType = settings.GetFieldType(fieldInfo);

                    object fieldValue;
                    switch (fieldType)
                    {
                        case "System.Guid":
                            fieldValue = !fieldInfo.IsLiteral && fieldInfo.IsInitOnly ? fieldInfo.GetValue(null).ToString() : null;
                            break;
                        default:
                            fieldValue = fieldInfo.IsLiteral && !fieldInfo.IsInitOnly ? fieldInfo.GetRawConstantValue() : null;
                            break;
                    }
                    tsType.Properties.Add(new TsProperty(fieldName, fieldType, tsType, fieldValue, fieldInfo.IsStatic));
                }
            }
        }

    }
}
