import { Browser, BrowserContext, Page, chromium } from "playwright-core";

//https://playwright.dev/docs/test-global-setup-teardown#example
async function globalSetup(){
    const browser: Browser = await chromium.launch();
    const context: BrowserContext = await browser.newContext();
    const page: Page = await context.newPage();
    await page.goto('https://www.saucedemo.com');
    await page.fill('[data-test="username"]', 'standard_user');
    await page.fill('[data-test="password"]', 'secret_sauce');
    await page.click('[data-test="login-button"]');
    await page.context().storageState({ path: './tests/temp/storageState.json' });
    await browser.close();
}

export default globalSetup;