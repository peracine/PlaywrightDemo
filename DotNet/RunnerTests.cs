using System;
using System.Threading.Tasks;
using Microsoft.Playwright.NUnit;

namespace PlaywrightDemo;

//cf: https://playwright.dev/dotnet/docs/test-runners#base-nunit-classes-for-playwright
[Parallelizable(ParallelScope.Self)]
public class RunnerTests : PageTest
{
    [Test]
    public async Task Testing_DOM_by_using_WebFirst_assertion()
    {
        await Page.GotoAsync("https://en.wikipedia.org/wiki/Harald_V_of_Norway");

        await Expect(Page.Locator("h1")).ToHaveTextAsync("Harald V of Norway");
    }

    [Test]
    public async Task Testing_HttpStattus_by_using_assert()
    {
        var response = await Page.GotoAsync("https://en.wikipedia.org/wiki/Harald_V_of_Norway");

        Assert.AreEqual(200, response.Status);
    }
}