/*
Testing without the need to log in each time (global-setup.js)
cf: https://playwright.dev/docs/test-auth#reuse-signed-in-state
*/
const { chromium, test, expect } = require('@playwright/test');

const _protectedUrl = 'https://www.saucedemo.com/inventory.html';
const _loginUrl = 'https://www.saucedemo.com';

test('Access to protected page with authentication', async ({ page }) => {
    await page.goto(_protectedUrl);
    expect(page.url()).toContain(_protectedUrl);
});

test('Access to protected page without authentication', async () => {
    const browser = await chromium.launch();
    const context = await browser.newContext();
    const page = await context.newPage();
    await page.goto(_protectedUrl);
    expect(page.url()).toContain(_loginUrl);
});