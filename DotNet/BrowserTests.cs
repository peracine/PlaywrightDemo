using System;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace PlaywrightDemo.Tests;

public class BrowserTests
{
    private string _testUrl = "https://toolbox.googleapps.com/apps/browserinfo";
    private bool _headless = false; //Set it to true to hide the browser when the tests are running
    private BrowserTypeLaunchOptions options { get; set; }

    [SetUp] 
    public void Init()
    { 
        options = new BrowserTypeLaunchOptions{ Headless = _headless, SlowMo = 10000 };
    }

    [Test]
    public async Task Testing_with_Chromium_returns_200()
    {
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(options);
        var page = await browser.NewPageAsync();

        var response = await page.GotoAsync(_testUrl);

        Assert.AreEqual(200, response.Status);
    }

    [Test]
    public async Task Testing_with_Firefox_returns_200()
    {
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Firefox.LaunchAsync(options);
        var page = await browser.NewPageAsync();

        var response = await page.GotoAsync(_testUrl);

        Assert.AreEqual(200, response.Status);
    }

    [Test]
    public async Task Testing_with_Webkit_returns_200()
    {
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Webkit.LaunchAsync(options);
        var page = await browser.NewPageAsync();

        var response = await page.GotoAsync(_testUrl);

        Assert.AreEqual(200, response.Status);
    }
}