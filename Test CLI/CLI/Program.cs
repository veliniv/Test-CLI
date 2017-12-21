using McMaster.Extensions.CommandLineUtils;
using System;

namespace CLI
{
    [HelpOption]
    [Command("test")]
    [Subcommand("create", typeof(CreateCommand))]
    public class Program
    {
        public static void Main(string[] args)
        {
            try{
                CommandLineApplication.Execute<Program>(args);
            }
            catch(Exception e){
                Console.WriteLine(e.Message);
            }
            
        }

        protected int OnExecute(CommandLineApplication app)
        {
            app.ShowHelp();
            return 1;
        }
    }
}
