using Microsoft.Playwright;

using QualiT.E2E.Support;

using Reqnroll;
using Reqnroll.BoDi;

namespace QualiT.E2E.Hooks;

[Binding]
public sealed class PlaywrightHooks(IObjectContainer container)
{
    private static IPlaywright _playwright = null!;
    private static IBrowser _browser = null!;
    private static readonly string ArtifactsDir = Path.Combine(AppContext.BaseDirectory, "artifacts");

    private IBrowserContext _context = null!;

    [BeforeTestRun]
    public static async Task BeforeTestRunAsync()
    {
        var exit = Microsoft.Playwright.Program.Main(["install", "chromium"]);
        if (exit != 0) throw new InvalidOperationException($"Playwright browser install failed (exit {exit}).");

        Directory.CreateDirectory(ArtifactsDir);
        _playwright = await Playwright.CreateAsync();
        _playwright.Selectors.SetTestIdAttribute(TestConfig.TestIdAttribute);
        _browser = await _playwright.Chromium.LaunchAsync(new() { Headless = TestConfig.Headless });
    }

    [BeforeScenario]
    public async Task BeforeScenarioAsync()
    {
        _context = await _browser.NewContextAsync(new() { BaseURL = TestConfig.BaseUrl });
        _context.SetDefaultTimeout(TestConfig.DefaultTimeoutMs);
        await _context.Tracing.StartAsync(new() { Screenshots = true, Snapshots = true, Sources = true });
        container.RegisterInstanceAs(await _context.NewPageAsync());
    }

    [AfterScenario]
    public async Task AfterScenarioAsync(ScenarioContext scenario, IReqnrollOutputHelper output)
    {
        var name = string.Concat(scenario.ScenarioInfo.Title.Split(Path.GetInvalidFileNameChars()));
        if (scenario.TestError is not null)
        {
            var screenshot = Path.Combine(ArtifactsDir, $"{name}.png");
            await container.Resolve<IPage>().ScreenshotAsync(new() { Path = screenshot, FullPage = true });
            await _context.Tracing.StopAsync(new() { Path = Path.Combine(ArtifactsDir, $"{name}.zip") });
            output.AddAttachment(screenshot);
        }
        else
        {
            await _context.Tracing.StopAsync();
        }
        await _context.CloseAsync();
    }

    [AfterTestRun]
    public static async Task AfterTestRunAsync()
    {
        await _browser.CloseAsync();
        _playwright.Dispose();
    }
}