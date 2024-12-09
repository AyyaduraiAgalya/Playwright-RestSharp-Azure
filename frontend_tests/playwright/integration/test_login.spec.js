import { test, expect } from '@playwright/test';

test('Login Test', async ({ page }) => {
  // Define locators
  const closeBanner = page.getByLabel('Close Welcome Banner');
  const emailField = page.getByLabel('Text field for the login email');
  const passwordField = page.getByLabel('Text field for the login password');
  const loginButton = page.getByLabel('Login', { exact: true });

  // Navigate to login page
  await page.goto('http://localhost:3000/#/login');

  // Close the banner and log in
  await closeBanner.click();
  await emailField.fill('admin@juice-sh.op');
  await passwordField.fill('admin123');
  await loginButton.click();

  // Verify successful login
  await expect(page).toHaveURL('http://localhost:3000/#/search');
});
