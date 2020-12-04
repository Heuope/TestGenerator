using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace TestGeneratorLib
{
    class Tester
    {
        private readonly FileIO _fileIO;
        private readonly Generator _generator;

        public Tester()
        {
            _fileIO = new FileIO();
            _generator = new Generator();
        }

        public Task GenerateTests(List<string> source, string destination, int maxParallelism)
        {
            var dataflowExecutionOption = new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = maxParallelism };
            var linkOptions = new DataflowLinkOptions { PropagateCompletion = true };

            var loadFile = new TransformBlock<string, string>(async path =>
                await _fileIO.ReadFileAsync(path), dataflowExecutionOption);

            var generateTest = new TransformBlock<string, List<TestFile>>(text =>
            {
                return _generator.CreateTest(text);
            }, dataflowExecutionOption);


            var saveFile = new ActionBlock<List<TestFile>>(async text =>
                await _fileIO.WriteFileAsync(destination, text), dataflowExecutionOption);

            loadFile.LinkTo(generateTest, linkOptions);
            generateTest.LinkTo(saveFile, linkOptions);

            foreach (var file in source)
            {
                loadFile.Post(file);
            }

            loadFile.Complete();

            return saveFile.Completion;
        }
    }
}