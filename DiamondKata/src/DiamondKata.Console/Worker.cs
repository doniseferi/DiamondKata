using DiamondKata.Domain.Builders;
using DiamondKata.Domain.ValueType;
using Microsoft.Extensions.Hosting;

namespace DiamondKata.Console;

internal class Worker : IHostedService
{
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    private readonly IDiamondQueryHandler _diamondQueryHandler;
    private readonly CommandLineOptions _commandLineOptions;

    public Worker(
        IHostApplicationLifetime hostApplicationLifetime,
        IDiamondQueryHandler diamondQueryHandler,
        CommandLineOptions commandLineOptions)
    {
        _hostApplicationLifetime = hostApplicationLifetime
                                   ?? throw new ArgumentNullException(nameof(hostApplicationLifetime));

        _diamondQueryHandler = diamondQueryHandler
                               ?? throw new ArgumentNullException(nameof(diamondQueryHandler));

        _commandLineOptions = commandLineOptions
                              ?? throw new ArgumentNullException(nameof(commandLineOptions));
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            System.Console.WriteLine(
                _diamondQueryHandler
                    .Handle(new DiamondRequest(
                        inputChar: new EnglishChar(_commandLineOptions.EnglishChar),
                        outerPaddingChar: new PaddingChar(' '),
                        innerPaddingChar: new PaddingChar('-'))));
        }
        catch (Exception)
        {
            Environment.ExitCode = (int)ExitCode.UnexpectedError;
        }
        finally
        {
            _hostApplicationLifetime.StopApplication();
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}