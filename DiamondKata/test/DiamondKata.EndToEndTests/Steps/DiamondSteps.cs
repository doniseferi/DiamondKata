using DiamondKata.EndToEndTests.Records;
using DiamondKata.EndToEndTests.TestHandler;
using TechTalk.SpecFlow;

namespace DiamondKata.EndToEndTests.Steps;

[Binding]
class DiamondSteps
{
    private readonly SystemUnderTestExecutionHandler _systemUnderTestExecutionHandler;
    private ConsoleApplicationExecutionResult _resultFromConsole;

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

    private async Task<ConsoleApplicationExecutionResult> GetApplicationResponseAsync(string[] args) =>
        await _systemUnderTestExecutionHandler.ExecuteAsync(args);
}