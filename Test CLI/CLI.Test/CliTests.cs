using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace CLI.Test
{
    [TestClass]
    public class CliTests
    {
        private Process process;
        private string testFolderPath;
        private string workingDirectory;
        private string solutionRootPath;

        [TestInitialize]
        public void Initialize()
        {
            var currenPath = Directory.GetCurrentDirectory();
            solutionRootPath = Directory.GetParent(currenPath).Parent.Parent.Parent.FullName;
            this.workingDirectory = Path.Combine(solutionRootPath, "CLI", "bin", "Debug", "netcoreapp2.0");

            // create Test folder, where file will be created. It will be deleted the test ends afterwards 
            this.testFolderPath = Path.Combine(currenPath, "Test");
            Directory.CreateDirectory(testFolderPath);

            this.process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true,
                    CreateNoWindow = true,
                    WorkingDirectory = this.workingDirectory
                }
            };
        }

        [TestCleanup]
        public void Cleanup()
        {
            Directory.Delete(this.testFolderPath, true);
        }

        [TestMethod]
        public void CreateResourcePackageTest()
        {
            var args = string.Format("cli.dll create");
            args = AddOptionToArguments(args, "-p", this.testFolderPath);

            this.process.StartInfo.Arguments = args;
            this.process.Start();

            StreamReader myStreamReader = process.StandardOutput;
            
            var expectedFilePath = Path.Combine(this.testFolderPath, "TestFile.txt");
            var outputString = myStreamReader.ReadToEnd();

            var expectedOutput = new StringBuilder();
            expectedOutput.AppendLine(string.Format("File created. Path: {0}", expectedFilePath));
            var message = string.Format("test Folder path: {0}, working dir: {1}, solution path {2}", testFolderPath, workingDirectory, solutionRootPath);
            //Assert.AreEqual(expectedOutput.ToString(), outputString, message);
            
            Assert.IsTrue(File.Exists(expectedFilePath));
        }

        private static string AddOptionToArguments(string args, string optionName, string optionValue)
        {
            return string.Format("{0} {1} \"{2}\"", args, optionName, optionValue);
        }
    }
}
