using System;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace PlaywrightDemo;

public class StandardTests
{
    [Test]
    public async Task Page_title_is_correct()
    {
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Firefox.LaunchAsync();
        var page = await browser.NewPageAsync();

        await page.GotoAsync("https://en.wikipedia.org/wiki/Harald_V_of_Norway");

        StringAssert.Contains("Harald V of Norway", await page.TitleAsync());
    }
}