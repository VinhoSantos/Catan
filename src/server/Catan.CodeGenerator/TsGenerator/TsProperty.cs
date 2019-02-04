namespace Catan.CodeGenerator.TsGenerator
{
    public class TsProperty
    {
        public TsType TsType { get; set; }

        public string Name { get; set; }
        public string Type { get; set; }
        public object Value { get; set; }
        public bool IsStatic { get; set; }

        public TsProperty(string name, string type, TsType tsType, object value, bool isStatic)
        {
            Name = name;
            Type = type;
            TsType = tsType;
            Value = value;
            IsStatic = isStatic;
        }
    }
}