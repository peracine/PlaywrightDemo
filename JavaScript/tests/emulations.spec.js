const { test, webkit, chromium, devices } = require('@playwright/test');

test('Emulate iPhone 13 Pro', async () => {
  const browser = await webkit.launch();
  const context = await browser.newContext({ ...devices['iPhone 13 Pro'] });
  const page = await context.newPage();
  await page.goto('https://www.wikipedia.org');
  await page.screenshot({ path: `temp/iPhone_13_Pro_${new Date().toISOString().split('T')[0]}.png`, fullPage: true });

  await context.close();
  await browser.close();
});

test('Emulate geolocations', async () => {
  const browser = await webkit.launch();
  const context = await browser.newContext({
    geolocation: { latitude: -33.85681, longitude: 151.21500 },
    permissions: ['geolocation']
  });

  const page = await context.newPage();
  await page.goto('https://browserleaks.com/geo');
  await page.waitForSelector('.flag_text');
  await page.screenshot({ path: `temp/geolocation_Sydney_${new Date().toISOString().split('T')[0]}.png`, fullPage: true });

  await context.setGeolocation({ latitude: 40.68925, longitude: -74.04455 });
  await page.waitForSelector('.flag_text');
  await page.screenshot({ path: `temp/geolocation_NewYork_${new Date().toISOString().split('T')[0]}.png`, fullPage: true });

  await context.close();
  await browser.close();
});

//Edge is based on Chromium since january 15, 2020.
test('Emulate MS Edge', async () => {
  const browser = await chromium.launch({ channel: 'msedge' });
  const context = await browser.newContext();
  const page = await context.newPage();
  await page.goto('https://toolbox.googleapps.com/apps/browserinfo');
  await page.screenshot({ path: `temp/MSEdge_${new Date().toISOString().split('T')[0]}.png`, fullPage: true });

  await context.close();
  await browser.close();
});

test('Emulate offline mode', async () => {
  const browser = await chromium.launch();
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