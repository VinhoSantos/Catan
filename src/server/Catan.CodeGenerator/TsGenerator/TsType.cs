using System.Collections.Generic;

namespace Catan.CodeGenerator.TsGenerator
{
    public class TsType
    {
        public TsModule TsModule { get; set; }

        public string Type { get; set; }
        public string Name { get; set; }
        public string BaseType { get; set; }
        public List<string> Interfaces { get; set; }
        public List<TsProperty> Properties { get; set; }

        public TsType(string type, string name, string baseType, List<string> interfaces, TsModule tsModule)
        {
            Type = type;
            Name = name;
            BaseType = baseType;
            Interfaces = interfaces;
            TsModule = tsModule;
            Properties = new List<TsProperty>();
        }
    }
}