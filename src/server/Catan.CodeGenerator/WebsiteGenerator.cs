using System;

namespace Catan.CodeGenerator
{
    public class WebsiteGenerator
    {
        public static void Generate()
        {
            GenerateTypescript(@"D:\Users\Bart\Documents\GitHub\Catan\src\client\src\scripts\data\contracts.ts");
        }

        private static void GenerateTypescript(string outputFile)
        {
            Console.Out.WriteLine($"Generating typescript data contracts: {outputFile}");
            TypeScriptGenerator.GenerateContracts(outputFile);
        }
    }
}
