using System.IO;
using TestGenerator;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace TestProject
{
    public class Pipeline
    {
        public Task Generate(int maxDegreeOfParallelism, string destination, params string[] paths)
        {
            var options = new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = maxDegreeOfParallelism };

            var openFile = new TransformBlock<string, string>(async filePath => await File.ReadAllTextAsync(filePath), options);
            var generateTests = new TransformManyBlock<string, TestElement>(fileCode => TestGenerator.TestGenerator.GenerateTests(fileCode), options);
            var writeToFile = new ActionBlock<TestElement>(async testElement => await File.WriteAllTextAsync(
                Path.Combine(destination, testElement.TestName) + ".cs", testElement.TestCode));

            var newOptions = new DataflowLinkOptions { PropagateCompletion = true };
            openFile.LinkTo(generateTests, newOptions);
            generateTests.LinkTo(writeToFile, newOptions);


            foreach (string path in paths)
                openFile.Post(path);
            openFile.Complete();

            return writeToFile.Completion;
        }
    }
}

