using Microsoft.Playwright;

namespace QualiT.E2E.Pages.Components;

public sealed class SidebarComponent(IPage page)
{
    private ILocator UsersButton => page.GetByRole(AriaRole.Button, new() { Name = "Users", Exact = true });

    public ILocator LogoutButton => page.GetByRole(AriaRole.Button, new() { Name = "Log Out", Exact = true });

    public Task OpenUsersAsync() => UsersButton.ClickAsync();
}