// See https://aka.ms/new-console-template for more information

using CommandLine;
using DiamondKata.Console;
using DiamondKata.Console.Extensions;
using DiamondKata.Domain.Exception;
using DiamondKata.Domain.ValueType;

await Parser.Default.ParseArguments<CommandLineOptions>(args)
    .WithParsedAsync(x =>
    {
        if (!x.EnglishChar.IsAnEnglishLetter())
            Environment.ExitCode = (int)ExitCode.InvalidEnglishChar;

        try
        {
            Console.WriteLine(new Diamond(new EnglishChar(char.ToUpperInvariant(x.EnglishChar)),
                new PaddingChar(' '),
                new PaddingChar('-')).Value);
        }
        catch (CharIsNotAnEnglishLetterException)
        {
            Environment.ExitCode = (int)ExitCode.InvalidEnglishChar;
        }

        return Task.CompletedTask;
    });