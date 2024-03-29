﻿// See https://aka.ms/new-console-template for more information

using CommandLine;
using DiamondKata.Console.Extensions;
using DiamondKata.DomainService.QueryHandlers;
using DiamondKata.DomainService.QueryHandlers.GetPadding;
using DiamondKata.DomainService.QueryHandlers.GetRowForChar;
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
            .WithParsedAsync(commandLineOptions =>
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
                            .GetService<IGetDiamondQueryHandler>();

                    try
                    {
                        WriteLine(
                            diamondQueryHandler
                                .Handle(new EnglishChar(commandLineOptions.EnglishChar)));
                    }
                    catch (Exception e)
                    {
                        WriteLine(e.Message);
                        throw;
                    }
                }

                return Task.CompletedTask;
            });
    }

    private static IServiceCollection RegisterServices() =>
        new ServiceCollection()
            .AddSingleton<IGetDiamondQueryHandler, GetDiamondQueryHandler>()
            .AddSingleton<IGetRowForCharQueryHandler, GetRowForCharQueryHandler>()
            .AddSingleton<IGetOuterPaddingQueryHandler, GetOuterPaddingQueryHandler>()
            .AddSingleton<IGetInnerPaddingQueryHandler, GetInnerPaddingQueryHandler>();
}