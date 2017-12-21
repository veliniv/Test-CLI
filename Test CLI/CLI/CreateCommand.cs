using McMaster.Extensions.CommandLineUtils;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace CLI
{
    [Command("create", Description = "Simple command that creates a file.")]
    public class CreateCommand
    {
        [Option("-p|--path", Description = "Path where the file will be created.")]
        [Required]
        public string FolderPath { get; set; }

        public int OnExecute(CommandLineApplication app)
        {
            var fileName = "TestFile.txt";
            var filePath = Path.Combine(FolderPath, fileName);
            File.WriteAllText(filePath, "hello :)");
            Console.WriteLine("File created. Path: {0}", filePath);
            return 0;
        }
    }
}
