using Microsoft.Extensions.Configuration;

namespace QualiT.E2E.Support;

public static class TestConfig
{
    private static readonly IConfiguration Config = new ConfigurationBuilder()
        .SetBasePath(AppContext.BaseDirectory)
        .AddJsonFile("appsettings.json", optional: false)
        .AddJsonFile("appsettings.local.json", optional: true)
        .AddEnvironmentVariables()
        .Build();

    public static string BaseUrl => Required("BaseUrl");
    public static bool Headless => !(bool.TryParse(Config["Headed"], out var headed) && headed);
    public static string TestIdAttribute => Config["TestIdAttribute"] ?? "data-testid";
    public static float DefaultTimeoutMs => float.TryParse(Config["DefaultTimeoutMs"], out var t) ? t : 10_000;

    public static Credentials UserFor(string persona) => new(
        Required($"Users:{persona}:Username"),
        Required($"Users:{persona}:Password"));

    public static string Required(string key) =>
        Config[key] ?? throw new InvalidOperationException(
            $"Missing config '{key}'. Set it in appsettings.local.json or as env var '{key.Replace(":", "__")}'.");
}

public sealed record Credentials(string Username, string Password);