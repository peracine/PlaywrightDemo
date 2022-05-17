using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightDemo.Tests;

//cf: https://playwright.dev/dotnet/docs/test-runners#base-nunit-classes-for-playwright
//cf: https://playwright.dev/dotnet/docs/selectors
//cf: https://playwright.dev/dotnet/docs/release-notes#web-first-assertions

[Parallelizable(ParallelScope.Self)]
public class RunnerTests : PageTest
{
    private string _testUrl = "https://en.wikipedia.org/wiki/Harald_V_of_Norway";
    private string _expectedText = "Harald V of Norway";
    
    [Test]
    public async Task Getting_header_by_CSS()
    {
        await Page.GotoAsync(_testUrl);

        await Expect(Page.Locator("h1")).ToHaveTextAsync(_expectedText);
    }

    [Test]
    public async Task Getting_header_by_XPath()
    {
        await Page.GotoAsync(_testUrl);

        await Expect(Page.Locator("//h1[@id='firstHeading']")).ToHaveTextAsync(_expectedText);
    }
}