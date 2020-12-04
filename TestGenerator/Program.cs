using System;
using System.Collections.Generic;
using System.IO;
using TestGeneratorLib;

namespace TestGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new Tester();
            string dirPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
            string dest = @$"{dirPath}\TestClassesResult";
            var sourceList = new List<string>()
            {
                @$"{dirPath}\TestClasses\Class1.cs"
            };
            test.GenerateTests(sourceList, dest, 5).Wait();
        }
    }
}
