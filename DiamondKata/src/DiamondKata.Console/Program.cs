// See https://aka.ms/new-console-template for more information

using CommandLine;
using DiamondKata.Console;
using DiamondKata.Console.Extensions;
using DiamondKata.Domain.Builders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

internal class Program
{
    static async Task Main(string[] args)
    {
        await Parser.Default.ParseArguments<CommandLineOptions>(args)
            .WithParsedAsync(async commandLineOptions =>
            {
                if (!commandLineOptions.EnglishChar.IsAnEnglishLetter())
                {
                    Console.WriteLine("Please input an english letter.");
                    Environment.ExitCode = (int) ExitCode.InvalidEnglishChar;
                }
                else
                {
                    using var host = CreateHostBuilder().Build();
                    await host.RunAsync();
                }
            });
    }

    private static IHostBuilder CreateHostBuilder() => Host.CreateDefaultBuilder().ConfigureServices(
        services =>
            services.AddHostedService<Worker>()
                .AddSingleton<IDiamondQueryHandler, DiamondQueryHandler>()
                .AddSingleton<IRowGeneratorQueryHandler, RowGeneratorQueryHandler>()
                .AddSingleton<IOuterPaddingStringGenerator, OuterPaddingStringGenerator>()
                .AddSingleton<IOuterPaddingLengthQueryHandler, OuterPaddingLengthQueryHandler>()
                .AddSingleton<IInnerPaddingStringGenerator, InnerPaddingStringGenerator>()
                .AddSingleton<IInnerPaddingLengthQueryHandler, InnerPaddingLengthQueryHandler>()
                .AddSingleton<IGetLowerEnglishLettersQueryHandlers, GetLowerEnglishLettersQueryHandlers>()
    );
}