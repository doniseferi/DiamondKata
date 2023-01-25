using CommandLine;

namespace DiamondKata.Console;

internal class CommandLineOptions
{
    [Value(index: 0, Required = true, HelpText = "A valid english character.")]
    public char EnglishChar { get; set; }
}