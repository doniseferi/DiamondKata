using DiamondKata.EndToEndTests.Records;
using DiamondKata.EndToEndTests.TestHandler;
using TechTalk.SpecFlow;

namespace DiamondKata.EndToEndTests.Steps;

[Binding]
class DiamondSteps
{
    private readonly SystemUnderTestExecutionHandler _systemUnderTestExecutionHandler;
    private ConsoleApplicationExecutionResult _resultFromConsole;
    private List<ConsoleApplicationExecutionResult> _errorResultsFromConsole;
    private List<char> _inputChars = new();

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
    public async Task WhenIProvideAValidEnglishCharacterToTheConsole()
    {
        _resultFromConsole = await GetApplicationResponseAsync(new[] {"f"});
    }

    [Then(@"a correctly formatted diamond is printed")]
    public void ThenACorrectlyFormattedDiamondIsPrinted()
    {
        Assert.That(_resultFromConsole.ConsoleOutput, Is.EqualTo(@"     A     
    B-B    
   C---C   
  D-----D  
 E-------E 
F---------F
 E-------E 
  D-----D  
   C---C   
    B-B    
     A     
"));

        Assert.That(_resultFromConsole.ResultCode, Is.EqualTo(0));
    }

    [When(@"The input is passed into the application")]
    public async Task WhenTheInputIsPassedIntoTheApplication() => _errorResultsFromConsole =
        (await Task.WhenAll(
            _inputChars
                .Select(async c =>
                    await GetApplicationResponseAsync(new[] {c.ToString()}))))
        .ToList();


    [Then(@"the application should exit with a non-zero System Error code")]
    public void ThenTheApplicationShouldExitWithANon_ZeroSystemErrorCode() =>
        _errorResultsFromConsole
            .ForEach(x =>
                Assert.That(x.ResultCode, Is.Not.Zero));

    [Given(@"an invalid english character is specified:")]
    public void GivenAnInvalidEnglishCharacterIsSpecified(Table table) =>
        _inputChars = table.Rows.Select(r => char.Parse(r[0])).ToList();

    [Then(@"the user is presented with a human readable message")]
    public void ThenTheUserIsPresentedWithAHumanReadableMessage()
    {
        var expectedErrorMessage = "Please input an english letter.";
        _errorResultsFromConsole
            .ForEach(x =>
                Assert.That(x.ConsoleOutput.Trim('\r', '\n'),
                    Is.EqualTo(expectedErrorMessage)));
    }

    private Task<ConsoleApplicationExecutionResult> GetApplicationResponseAsync(string[] args) =>
        _systemUnderTestExecutionHandler.ExecuteAsync(args);
}