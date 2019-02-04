using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Catan.CodeGenerator.TsGenerator
{
    public abstract class TsGeneratorSettings
    {
        public abstract IEnumerable<Type> GetTypes();

        public virtual string GetTypeName(Type type) { return type.Name; }

        public virtual string GetModuleName(string @namespace) { return @namespace; }

        public virtual bool MapProperty(Type type, PropertyInfo propertyInfo) { return propertyInfo.DeclaringType == type; }
        public virtual bool MapField(FieldInfo fieldInfo) { return true; }

        public virtual string GetPropertyName(PropertyInfo propertyInfo) { return propertyInfo.Name; }
        public virtual string GetPropertyType(PropertyInfo propertyInfo) { return GetType(propertyInfo.PropertyType); }

        public virtual string GetFieldName(FieldInfo fieldInfo) { return fieldInfo.Name; }
        public virtual string GetFieldType(FieldInfo fieldInfo) { return GetType(fieldInfo.FieldType); }
        public virtual string GetEnumName(FieldInfo fieldInfo) { return fieldInfo.Name; }

        protected string GetCamelCase(string input)
        {
            return char.ToLower(input[0]) + input.Substring(1);
        }

        public virtual string GetBaseType(Type type)
        {
            if (type == null) return null;
            if (type == typeof(object)) return null;
            if (type == typeof(Enum)) return null;
            if (type == typeof(ValueType)) return null;

            return GetModuleName(type.Namespace) + "." + type.Name;
        }

        protected string GetType(Type type)
        {
            if (type == typeof(object)) return null;
            if (type == typeof(string)) return "string";
            if (type == typeof(bool)) return "boolean";
            if (type == typeof(decimal)) return "number";
            if (type == typeof(byte)) return "number";
            if (type == typeof(int)) return "number";
            if (type == typeof(long)) return "number";
            if (type == typeof(short)) return "number";
            if (type == typeof(float)) return "number";
            if (type == typeof(double)) return "number";
            if (type == typeof(DateTime)) return "Date";

            if (type.IsGenericType)
            {
                if (type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    var enumerableType = type.GetGenericArguments()[0];
                    return GetType(enumerableType) + "[]";
                }
                if (type.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    var nullableType = type.GetGenericArguments()[0];
                    return GetType(nullableType);
                }
            }

            foreach (var @interface in type.GetInterfaces())
            {
                if (@interface.IsGenericType)
                {
                    if (@interface.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                    {
                        var enumerableType = @interface.GetGenericArguments()[0];
                        return GetType(enumerableType) + "[]";
                    }
                }
            }
            return GetModuleName(type.Namespace) + "." + type.Name;
        }

        public virtual IEnumerable<Type> GetReferencedTypes(Type type)
        {
            if (type.IsPrimitive) return new List<Type>();
            if (type == typeof(object)) return new List<Type>();
            if (type == typeof(string)) return new List<Type>();
            if (type == typeof(decimal)) return new List<Type>();
            if (type == typeof(DateTime)) return new List<Type>();
            if (type == typeof(Guid)) return new List<Type>();

            if (type.IsGenericType)
            {
                if (type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    var enumerableType = type.GetGenericArguments()[0];
                    return GetReferencedTypes(enumerableType);
                }
                if (type.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    var nullableType = type.GetGenericArguments()[0];
                    return GetReferencedTypes(nullableType);
                }
            }

            foreach (var @interface in type.GetInterfaces())
            {
                if (@interface.IsGenericType)
                {
                    if (@interface.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                    {
                        var enumerableType = @interface.GetGenericArguments()[0];
                        return GetReferencedTypes(enumerableType);
                    }
                }
            }

            return new List<Type> { type };
        }

        public virtual string GetValueString(object value)
        {
            if (value == null) return "null";

            var valueString = value as string;
            if (valueString != null)
                return "\'" + valueString + "\'";

            return value.ToString();
        }

        public virtual void WriteModules(StringBuilder result, IEnumerable<TsModule> modules)
        {
            foreach (var module in modules.OrderBy(x => x.Name))
            {
                WriteModule(result, module);
            }
        }

        public virtual void WriteModule(StringBuilder result, TsModule module)
        {
            result.Append($"export module {module.Name} {{\r\n");
            WriteModuleContent(result, module);
            result.Append("}\r\n");
        }

        public virtual void WriteModuleContent(StringBuilder result, TsModule module)
        {
            foreach (var type in module.Types)
            {
                WriteType(result, type);
            }
        }

        public virtual void WriteType(StringBuilder result, TsType type)
        {
            result.Append($"    export {type.Type} {type.Name}");

            if (type.BaseType != null)
                result.Append($" extends {type.BaseType}");

            if (type.Interfaces.Any())
            {
                var inheritanceType = type.Type == "interface" ? "extends" : "implements";
                result.Append($" {inheritanceType} {string.Join(", ", type.Interfaces)}");
            }

            result.Append(" {\r\n");
            WriteTypeContent(result, type);
            result.Append("    }\r\n");
        }

        public virtual void WriteTypeContent(StringBuilder result, TsType type)
        {
            foreach (var property in type.Properties)
            {
                WriteProperty(result, property);
            }
        }

        public virtual void WriteProperty(StringBuilder result, TsProperty property)
        {
            result.Append("        ");

            if (property.IsStatic)
                result.Append("static ");

            result.Append(property.Name);

            if (!string.IsNullOrEmpty(property.Type))
                result.Append($": {GetTypescriptType(property)}");

            if (property.Value != null)
                result.Append($" = {GetValueString(property.Value)}");

            result.Append($"{(property.TsType.Type == "enum" ? "," : ";")}\r\n");
        }

        public virtual string GetTypescriptType(TsProperty property)
        {
            return property.Type;
        }

    }

}