using System.Text.RegularExpressions;

using Microsoft.Playwright;

using QualiT.E2E.Pages.Components;
using QualiT.E2E.Pages.Users;

using Reqnroll;

using static Microsoft.Playwright.Assertions;

namespace QualiT.E2E.StepDefinitions;

[Binding]
public sealed class UsersSteps(SidebarComponent sidebar, UsersPage usersPage, IPage page)
{
    [When("the user opens the Users page")]
    public Task WhenTheUserOpensTheUsersPageAsync() => sidebar.OpenUsersAsync();

    [Then("the Users page is displayed")]
    public Task ThenTheUsersPageIsDisplayedAsync() => Expect(page).ToHaveURLAsync(new Regex("/manage/users$"));

    [Then("the user management actions and filters are available")]
    public async Task ThenTheUserManagementActionsAndFiltersAreAvailableAsync()
    {
        await Expect(usersPage.InviteUserButton).ToBeVisibleAsync();
        await Expect(usersPage.RebuildPermissionsButton).ToBeVisibleAsync();
        await Expect(usersPage.SearchBox).ToBeVisibleAsync();
        await Expect(usersPage.OrganizationsFilter).ToBeVisibleAsync();
        await Expect(usersPage.RolesFilter).ToBeVisibleAsync();
        await Expect(usersPage.StatusFilter).ToBeVisibleAsync();
    }
}