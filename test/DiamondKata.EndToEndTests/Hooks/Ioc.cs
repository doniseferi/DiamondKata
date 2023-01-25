using BoDi;
using DiamondKata.EndToEndTests.TestHandler;
using DiamondKata.EndToEndTests.Extensions;
using TechTalk.SpecFlow;

namespace DiamondKata.EndToEndTests.Hooks;

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