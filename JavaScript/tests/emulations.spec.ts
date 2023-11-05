import { test, webkit, devices } from '@playwright/test';

test('Emulating iPhone Pro 14', async () => {
  const browser = await webkit.launch();
  const context = await browser.newContext({ ...devices['iPhone 14 Pro'] });
  const page = await context.newPage();
  await page.goto('https://www.wikipedia.org');
  await page.screenshot({ path: `temp/iPhone_14_Pro_${new Date().toISOString().split('T')[0]}.png`, fullPage: true });

  await context.close();
  await browser.close();

});

test('Emulating offline mode', async () => {
  const browser = await webkit.launch();
  const context = await browser.newContext();
  const page = await context.newPage();
  const testUrl = 'https://en.wikipedia.org/wiki/Harald_V_of_Norway';
  await page.goto(testUrl);
  await context.setOffline(true);

  try {
    await page.goto(testUrl);
  }
  catch (exception) {
    if (exception.message.includes('ERR_INTERNET_DISCONNECTED')) {
      console.log('The application can not work offline');
    }
    else {
      console.log(exception.message);
    }
  }

  await context.close();
  await browser.close();
});
