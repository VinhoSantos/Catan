using System.Collections.Generic;

namespace Catan.CodeGenerator.TsGenerator
{
    public class TsModule
    {
        public string Name { get; set; }
        public List<TsType> Types { get; set; }

        public TsModule(string name)
        {
            Name = name;
            Types = new List<TsType>();
        }
    }
}