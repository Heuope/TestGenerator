using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using TestGeneratorLib;

namespace TestGeneratorLib.Test
{
    public class Tests
    {
        private Tester _tester;
        private string _directory;
        private string _dest;
        private List<string> _sourceList;

        private string GetThisFilePath([CallerFilePath] string path = null)
        {
            return path;
        }

        [SetUp]
        public void Setup()
        {
            var path = GetThisFilePath();
            _directory = Path.GetDirectoryName(path);

            _tester = new Tester();

            _dest = @$"{_directory}\TestResult";

            _sourceList = new List<string>()
            {
                @$"{_directory}\TestedClass.cs"
            };
        }

        [Test]
        public void CheckCountOfCreatedClasses()
        {
            _tester.GenerateTests(_sourceList, _dest, 5).Wait();

            int expected = 1;
            int actual = Directory.GetFiles(_dest).Length;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CompareTestCode()
        {
            _tester.GenerateTests(_sourceList, _dest, 5).Wait();

            string expected = File.ReadAllText(@$"{_directory}\expectedCode.txt");

            string actual = File.ReadAllText(@$"{_directory}\TestResult\TestedClass.cs");

            Assert.AreEqual(expected, actual);
        }
    }
}