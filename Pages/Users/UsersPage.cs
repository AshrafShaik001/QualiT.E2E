using Microsoft.Playwright;

namespace QualiT.E2E.Pages.Users;

public sealed class UsersPage(IPage page) : BasePage(page)
{
    protected override string Path => "/manage/users";

    public ILocator InviteUserButton => Page.GetByRole(AriaRole.Button, new() { Name = "Invite User" });
    public ILocator RebuildPermissionsButton => Page.GetByRole(AriaRole.Button, new() { Name = "Rebuild QS Permissions" });
    public ILocator SearchBox => Page.GetByPlaceholder("Search for users...");
    public ILocator OrganizationsFilter => Page.GetByTitle("All Organizations", new() { Exact = true });
    public ILocator RolesFilter => Page.GetByTitle("All Roles", new() { Exact = true });
    public ILocator StatusFilter => Page.GetByTitle("Status", new() { Exact = true });
}