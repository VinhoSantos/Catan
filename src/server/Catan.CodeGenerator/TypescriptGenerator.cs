using Catan.Core.Extensions;
using Catan.CodeGenerator.TsGenerator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Catan.CodeGenerator
{
    public class TypeScriptGenerator
    {
        public static void GenerateContracts(string outputFile)
        {
            var output = TsGenerator.TsGenerator.Generate(new CatanTsGeneratorSettings());
            File.WriteAllText(outputFile, output);
        }

        private class CatanTsGeneratorSettings : TsGeneratorSettings
        {
            public override IEnumerable<Type> GetTypes()
            {
                var assembly = Assembly.Load("Catan.API");
                
                Type[] types;
                try
                {
                    types = assembly.GetTypes();
                }
                catch (ReflectionTypeLoadException e)
                {
                    types = e.Types;
                }

                return types.Where(t => t != null).Where(MapType);
            }

            private static bool MapType(Type type)
            {
                return type.GetCustomAttributes(typeof(CodeGeneratorAttribute), true).Length > 0;
            }

            public override string GetModuleName(string @namespace)
            {
                return @namespace.Replace("Catan.Core", "Scripts.Data");
            }

            public override void WriteModuleContent(StringBuilder result, TsModule module)
            {
                result.Append("    'use strict';\r\n");
                base.WriteModuleContent(result, module);
            }

            public override string GetPropertyName(PropertyInfo propertyInfo)
            {
                return GetCamelCase(propertyInfo.Name);
            }

            public override string GetFieldName(FieldInfo fieldInfo)
            {
                return GetCamelCase(fieldInfo.Name);
            }

            public override void WriteTypeContent(StringBuilder result, TsType type)
            {
                if (type.Name.EndsWith("Request") || type.Name.EndsWith("Command"))
                {
                    var controllerStart = type.TsModule.Name.Replace("Scripts.Data.", "").Replace(".", "_");
                    var controllerEnd = type.Name;
                    if (controllerEnd.EndsWith("Request"))
                    {
                        controllerEnd = controllerEnd.Substring(0, controllerEnd.Length - "Request".Length);
                    }
                    if (controllerEnd.EndsWith("Command"))
                    {
                        controllerEnd = controllerEnd.Substring(0, controllerEnd.Length - "Command".Length);
                    }

                    if (type.Type != "interface")
                        result.Append(string.Format("        path='{0}_{1}';\r\n", controllerStart, controllerEnd));
                }
                base.WriteTypeContent(result, type);
            }

            public override string GetTypescriptType(TsProperty property)
            {
                if (property.Type == "System.Guid")
                    return "Guid";

                if (property.Type == "System.Guid[]")
                    return "Guid[]";

                return base.GetTypescriptType(property);
            }

        }

    }
}
