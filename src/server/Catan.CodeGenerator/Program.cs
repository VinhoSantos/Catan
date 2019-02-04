using System;

namespace Catan.CodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Start Code Generator: {DateTime.Now}");
            WebsiteGenerator.Generate();
            Console.WriteLine($"Finished Code Generator: {DateTime.Now}");
            Console.ReadKey();
        }
    }
}
