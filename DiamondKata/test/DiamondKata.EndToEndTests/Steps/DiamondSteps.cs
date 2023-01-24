using DiamondKata.EndToEndTests.Records;
using DiamondKata.EndToEndTests.TestHandler;
using TechTalk.SpecFlow;

namespace DiamondKata.EndToEndTests.Steps;

internal class DiamondSteps
{
    private readonly SystemUnderTestExecutionHandler _systemUnderTestExecutionHandler;
    private string _resultFromConsole;

    public DiamondSteps(SystemUnderTestExecutionHandler systemUnderTestExecutionHandler)
    {
        _systemUnderTestExecutionHandler = systemUnderTestExecutionHandler ??
                                           throw new ArgumentNullException(nameof(systemUnderTestExecutionHandler));
    }

    [Given(@"I want to print a diamond onto console")]
    public void GivenIWantToPrintADiamondOntoConsole()
    {
    }

    [When(@"I provide a valid english character to the console")]
    public void WhenIProvideAValidEnglishCharacterToTheConsole()
    {
    }

    [Then(@"a correctly formatted diamond is printed")]
    public void ThenACorrectlyFormattedDiamondIsPrinted()
    {
        throw new PendingStepException();
    }

    //private async Task<List<ConsoleApplicationExecutionResult>> GetApplicationResponse(char englishChar, char outerPadding, char innerPadding) =>
    //    (await Task.WhenAll(
    //        englishChar
    //            .Select(
    //                async id => await _systemUnderTestExecutionHandler.ExecuteAsync(new[] { id }))))
    //    .ToList();

}