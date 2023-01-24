// See https://aka.ms/new-console-template for more information

using CommandLine;
using DiamondKata.Console.Extensions;
using DiamondKata.Domain.Builders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DiamondKata.Console;

internal class Program
{
    static async Task Main(string[] args)
    {
        await Parser.Default.ParseArguments<CommandLineOptions>(args)
            .WithParsedAsync(async commandLineOptions =>
            {
                if (!commandLineOptions.EnglishChar.IsAnEnglishLetter())
                {
                    System.Console.WriteLine("Please input an english letter.");
                    Environment.ExitCode = (int)ExitCode.InvalidEnglishChar;
                }
                else
                {
                    using var host = CreateHostBuilder(commandLineOptions).Build();
                    await host.RunAsync();
                }
            });
    }

    private static IHostBuilder CreateHostBuilder(CommandLineOptions commandLineOptions)
    {
        return Host.CreateDefaultBuilder()
            .ConfigureServices(
                services =>
                {
                    services.AddHostedService<Worker>()
                        .AddSingleton(commandLineOptions)
                        .AddSingleton<IDiamondQueryHandler, DiamondQueryHandler>()
                        .AddSingleton<IRowGeneratorQueryHandler, RowGeneratorQueryHandler>()
                        .AddSingleton<IOuterPaddingStringGenerator, OuterPaddingStringGenerator>()
                        .AddSingleton<IOuterPaddingLengthQueryHandler, OuterPaddingLengthQueryHandler>()
                        .AddSingleton<IInnerPaddingLengthQueryHandler, InnerPaddingLengthQueryHandler>()
                        .AddSingleton<IInnerPaddingStringGenerator, InnerPaddingStringGenerator>()
                        .AddSingleton<IGetLowerEnglishLettersQueryHandlers, GetLowerEnglishLettersQueryHandlers>();
                }).ConfigureLogging(logging => logging.ClearProviders());
    }
}