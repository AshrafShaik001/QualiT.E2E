# QualiT.E2E

End-to-end UI tests for the Curvo QA app (`https://cx-qa-app.curvolabs.com`), built with **Playwright + C# (.NET 10) + Reqnroll (BDD) + NUnit**.

## Prerequisites

- .NET SDK 8 or later (developed on 10.0)
- Allure CLI, for the Allure report only: `brew install allure` (macOS)
- Playwright's Chromium is installed automatically on the first test run.

## Configuration

| File | Purpose | Committed |
|---|---|---|
| `appsettings.json` | `BaseUrl`, `Headed`, `TestIdAttribute`, `DefaultTimeoutMs` | yes |
| `appsettings.local.json` | Login credentials | **no** (git-ignored) |
| `allureConfig.json` | Allure results folder | yes |

Create `appsettings.local.json` yourself (it is not in the repo):

```json
{ "Users": { "Admin": { "Username": "<email>", "Password": "<password>" } } }
```

Any setting can also be passed as an environment variable. Nested keys use a double underscore, e.g. `Users__Admin__Password`.

## Running the tests

Run these from the project folder.

| Goal | Command |
|---|---|
| All tests | `dotnet test` |
| Headed (visible browser) | `Headed=true dotnet test` |
| One test case | `dotnet test --filter "Category=TC-CL-01"` |
| By suite or module | `dotnet test --filter "Category=smoke"` / `"Category=users"` |
| Verbose step output | `dotnet test --logger "console;verbosity=detailed"` |

On Windows PowerShell, set variables like this: `$env:Headed="true"; dotnet test`.

## Test cases

| ID | Scenario | Feature file | Tags |
|---|---|---|---|
| TC-CL-01 (CL#01) | Login with valid credentials | `Features/Login/Login.feature` | `@smoke @login` |
| TC-CL-02 (CL#02) | Navigate to Users page and verify page view | `Features/Users/Users.feature` | `@smoke @users` |

## Reports

### 1. HTML report (quickest)

```
dotnet test --logger "html;LogFileName=report.html" --results-directory TestResults
open TestResults/report.html        # Windows: start TestResults\report.html
```

Shows each test with its status, duration and error details. Output: `TestResults/report.html`.

### 2. Allure report (feature, scenario and step breakdown)

Every `dotnet test` run writes raw results to `bin/Debug/net10.0/allure-results`. Build and open the report with:

```
allure generate bin/Debug/net10.0/allure-results -o allure-report --clean
allure open allure-report
```

Or in one step, which serves the report from a temporary folder:

```
allure serve bin/Debug/net10.0/allure-results
```

The results folder is **not cleared between runs**, so old results accumulate in the report. Delete `bin/Debug/net10.0/allure-results` before a run if you want a clean report.

### 3. Failure artifacts

When a scenario fails, the hooks save these to `bin/Debug/net10.0/artifacts/`:

- `<scenario name>.png`, a full-page screenshot
- `<scenario name>.zip`, a Playwright trace

Open a trace with `playwright show-trace <file>.zip`. The CLI comes from `dotnet tool install --global Microsoft.Playwright.CLI`.

## Project layout

```
Features/<Module>/*.feature     Gherkin scenarios
Pages/                          Page objects (Pages/Components for shared UI such as the sidebar)
StepDefinitions/                Thin step bindings
Hooks/PlaywrightHooks.cs        Browser, context, tracing and failure screenshots
Support/TestConfig.cs           Configuration and credential lookup
```

## Notes

- After the password step, the app shows a "Stay signed in?" prompt on every login. The login page object clicks **No**.
- Credentials never appear in feature files. Scenarios refer to a persona ("an admin"), which is resolved from `Users:Admin` in config.
- Generated folders (`bin/`, `obj/`, `TestResults/`, `allure-results/`, `allure-report/`, `artifacts/`) are git-ignored.
