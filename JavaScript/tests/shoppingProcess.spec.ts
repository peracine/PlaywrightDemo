import { test, expect } from '@playwright/test';

const _inventoryUrl = 'https://www.saucedemo.com/inventory.html';

test.describe('Testing the shopping process flow', () => {

  test('Accessing directly to the "inventory" page when authenticated', async ({ page }) => {
    await page.goto(_inventoryUrl);
    expect(page.url()).toBe(_inventoryUrl);
  });

  test('Choosing an item, should redirect to the "inventory-item" page', async ({ page }) => {
    await page.goto(_inventoryUrl);
    await page.locator('text=Sauce Labs Backpack').click();//Choosing an item from the list

    expect(page.url()).toContain('inventory-item');
  });

  test('Adding an item, the cart should be updated', async ({ page }) => {
    await page.goto(_inventoryUrl);
    await page.locator('[data-test="add-to-cart-sauce-labs-backpack"]').click();//Adding this item from the list
    await page.locator('span:has-text("1")').click();//Going to cart.html

    expect(page.url()).toBe('https://www.saucedemo.com/cart.html');
    expect(await page.locator('text=Sauce Labs Backpack').count()).toBe(1);//Finding this item in the list
  });

});
