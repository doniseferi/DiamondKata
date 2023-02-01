// See https://aka.ms/new-console-template for more information

using CommandLine;
using DiamondKata.Console.Extensions;
using DiamondKata.DomainService.Factories;
using DiamondKata.DomainService.QueryHandlers;
using DiamondKata.DomainService.Requests;
using DiamondKata.DomainService.ValueType;
using Microsoft.Extensions.DependencyInjection;
using static System.Console;

namespace DiamondKata.Console;

internal class Program
{
    static async Task Main(string[] args)
    {
        RegisterServices();

        await Parser.Default.ParseArguments<CommandLineOptions>(args)
            .WithParsedAsync(async commandLineOptions =>
            {
                if (!commandLineOptions.EnglishChar.IsAnEnglishLetter())
                {
                    WriteLine("Please input an english letter.");
                }
                else
                {
                    var diamondQueryHandler =
                        RegisterServices()
                            .BuildServiceProvider()
                            .GetService<IDiamondQueryHandler>();

                    WriteLine(
                        diamondQueryHandler
                            .Handle(new DiamondRequest(
                                inputChar: new EnglishChar(commandLineOptions.EnglishChar))));
                }
            });
    }

    private static IServiceCollection RegisterServices() =>
        new ServiceCollection()
            .AddSingleton<IDiamondQueryHandler, DiamondQueryHandler>()
            .AddSingleton<IRowGeneratorQueryHandler, RowGeneratorQueryHandler>()
            .AddSingleton<IOuterPaddingStringFactory, OuterPaddingStringFactory>()
            .AddSingleton<IOuterPaddingLengthQueryHandler, OuterPaddingLengthQueryHandler>()
            .AddSingleton<IInnerPaddingLengthQueryHandler, InnerPaddingLengthQueryHandler>()
            .AddSingleton<IInnerPaddingStringFactory, InnerPaddingStringFactory>()
            .AddSingleton<IGetLowerEnglishLettersQueryHandlers, GetLowerEnglishLettersQueryHandlers>();
}