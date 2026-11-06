using Microsoft.Playwright;

namespace QualiT.E2E.Pages;

public abstract class BasePage(IPage page)
{
    protected IPage Page { get; } = page;

    /// <summary>Path relative to BaseUrl, e.g. "/login".</summary>
    protected abstract string Path { get; }

    public Task OpenAsync() => Page.GotoAsync(Path);
}