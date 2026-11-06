using Microsoft.Playwright;

using QualiT.E2E.Pages.Components;
using QualiT.E2E.Pages.Login;
using QualiT.E2E.Support;

using Reqnroll;

using static Microsoft.Playwright.Assertions;

namespace QualiT.E2E.StepDefinitions;

[Binding]
public sealed class LoginSteps(LoginPage loginPage, SidebarComponent sidebar)
{
    private const string AdminPersona = "Admin";

    [Given("the user is on the login page")]
    public Task GivenTheUserIsOnTheLoginPageAsync() => loginPage.OpenAsync();

    [Given("the user is signed in as an admin")]
    public async Task GivenTheUserIsSignedInAsAnAdminAsync()
    {
        await loginPage.OpenAsync();
        await WhenTheUserSignsInAsAnAdminAsync();
    }

    [When("the user signs in as an admin")]
    public Task WhenTheUserSignsInAsAnAdminAsync()
    {
        var admin = TestConfig.UserFor(AdminPersona);
        return loginPage.LoginAsync(admin.Username, admin.Password);
    }

    [Then("the user is logged in")]
    public Task ThenTheUserIsLoggedInAsync() => Expect(sidebar.LogoutButton).ToBeVisibleAsync();
}