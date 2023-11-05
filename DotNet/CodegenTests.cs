using Microsoft.Playwright;

namespace PlaywrightDemo.Tests;

//cf: https://playwright.dev/dotnet/docs/codegen

public class CodegenTests
{
    //Get the generated code by executing the following command
    //>> pwsh bin\Debug\net6.0\playwright.ps1 codegen saucedemo.com
    [Test]
    public async Task Login_portal_returns_to_inventory_page()
    {
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false
        });
        var context = await browser.NewContextAsync();
        // Open new page
        var page = await context.NewPageAsync();
        // Go to https://www.saucedemo.com/
        await page.GotoAsync("https://www.saucedemo.com/");
        // Click [data-test="username"]
        await page.Locator("[data-test=\"username\"]").ClickAsync();
        // Fill [data-test="username"]
        await page.Locator("[data-test=\"username\"]").FillAsync("standard_user");
        // Click [data-test="password"]
        await page.Locator("[data-test=\"password\"]").ClickAsync();
        // Fill [data-test="password"]
        await page.Locator("[data-test=\"password\"]").FillAsync("secret_sauce");
        // Click [data-test="login-button"]
        await page.Locator("[data-test=\"login-button\"]").ClickAsync();
        Assert.That(page.Url, Is.EqualTo("https://www.saucedemo.com/inventory.html"));
    }

    [Test]
    public async Task Login_portal_returns_error()
    {
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false
        });
        var context = await browser.NewContextAsync();
        // Open new page
        var page = await context.NewPageAsync();
        // Go to https://www.saucedemo.com/
        await page.GotoAsync("https://www.saucedemo.com/");
        // Click [data-test="username"]
        await page.Locator("[data-test=\"username\"]").ClickAsync();
        // Fill [data-test="username"]
        await page.Locator("[data-test=\"username\"]").FillAsync("standard_user");
        // Click [data-test="password"]
        await page.Locator("[data-test=\"password\"]").ClickAsync();
        // Fill [data-test="password"]
        await page.Locator("[data-test=\"password\"]").FillAsync("BAD_PASSWORD");
        // Click [data-test="login-button"]
        await page.Locator("[data-test=\"login-button\"]").ClickAsync();

        var result = await page.IsVisibleAsync("[data-test=\"error\"]");

        Assert.That(result, Is.True);
    }
}