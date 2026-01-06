import { test, expect } from '@playwright/test';

test('create user and display in list', async ({ page }) => {
  await page.goto('file:///E:/Test/UserManagementFrontend/index.html');

  await page.fill('#name', 'Hung Dang 123');
  await page.fill('#email', 'hung@example.com');
  await page.click('button');

  // Wait for row to appear
  const rows = page.locator('#users tr');

  await expect(page.locator('#users')).toContainText('Hung Dang 123');
});
