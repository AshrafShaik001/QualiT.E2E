using Microsoft.Playwright;

namespace QualiT.E2E.Pages.Login;

public sealed class LoginPage(IPage page) : BasePage(page)
{
    protected override string Path => "/";

    private ILocator EmailInput => Page.GetByPlaceholder("Email address");
    private ILocator NextButton => Page.GetByRole(AriaRole.Button, new() { Name = "Next" });
    private ILocator PasswordInput => Page.GetByPlaceholder("Password");
    private ILocator SignInButton => Page.GetByRole(AriaRole.Button, new() { Name = "Sign in" });
    private ILocator StaySignedInDeclineButton => Page.GetByRole(AriaRole.Button, new() { Name = "No", Exact = true });

    /// <summary>Completes the two-step sign-in and declines the "Stay signed in?" prompt.</summary>
    public async Task LoginAsync(string username, string password)
    {
        await EmailInput.FillAsync(username);
        await NextButton.ClickAsync();
        await PasswordInput.FillAsync(password);
        await SignInButton.ClickAsync();
        await StaySignedInDeclineButton.ClickAsync();
    }
}