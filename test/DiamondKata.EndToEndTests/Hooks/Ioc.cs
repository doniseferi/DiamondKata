using BoDi;
using DiamondKata.Console.EndToEndTests.Extensions;
using DiamondKata.Console.EndToEndTests.TestHandler;
using TechTalk.SpecFlow;

namespace DiamondKata.Console.EndToEndTests.Hooks;

[Binding]
public class Ioc
{
    private readonly IObjectContainer _objectContainer;

    public Ioc(IObjectContainer objectContainer) => _objectContainer = objectContainer;

    [BeforeScenario]
    public void RegisterComponents()
    {
        _objectContainer.RegisterInstanceAs(
            new SystemUnderTestExecutionHandler(
                AppDomain.CurrentDomain.GetConsoleAppExePath()));
    }
}