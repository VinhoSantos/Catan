using System;

namespace Catan.CodeGenerator
{
    public class WebsiteGenerator
    {
        public static void Generate()
        {
            GenerateTypescript(@"C:\Users\bart_\Documents\Github\Catan\src\client\src\scripts\data\contracts.ts");
            GenerateTypescriptEvents(@"C:\Users\bart_\Documents\Github\Catan\src\client\src\scripts\data\events.ts");
        }

        private static void GenerateTypescript(string outputFile)
        {
            Console.Out.WriteLine($"Generating typescript data contracts: {outputFile}");
            TypeScriptGenerator.Generate(outputFile, false);
        }

        private static void GenerateTypescriptEvents(string outputFile)
        {
            Console.Out.WriteLine($"Generating typescript events: {outputFile}");
            TypeScriptGenerator.Generate(outputFile, true);
        }
    }
}
