using CommandLine;

namespace Lab1.CommandLineOptions
{
    /// <summary>
    /// Class to receive parsed values.
    /// </summary>
    internal class Options
    {
        [Option('i', "input", Required = true, HelpText = "Input file to be processed.")]

        public string InputFile { get; set; }

        [Option('o', "output", Required = true, HelpText = "Input file to be processed.")]
        public string OutputFile { get; set; }

        [Option('f', "mode", Required = true, HelpText = "File mode (JSON or Excel)")]

        public FileMode Mode { get; set; }
    }
}
