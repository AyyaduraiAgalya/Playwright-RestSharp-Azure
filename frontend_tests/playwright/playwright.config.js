require('dotenv').config();

const browserstackUsername = process.env.BROWSERSTACK_USERNAME;
const browserstackAccessKey = process.env.BROWSERSTACK_ACCESS_KEY;

const { defineConfig } = require('@playwright/test');

module.exports = defineConfig({
    testDir: './integration', // Test directory for your test scripts
    retries: 2, // Retry failed tests twice
    timeout: 120000, // Increase timeout for tests (60 seconds)
    reporter: [['html', { outputFolder: 'playwright-report' }]], // Generate HTML reports
    projects: [
        {
            name: 'chrome',
            use: {
                browserName: 'chromium',
                channel: 'chrome',
                baseURL: 'http://localhost:3000', // Local environment
                headless: true,
            },
        },
        {
            name: 'firefox',
            use: {
                browserName: 'firefox',
                baseURL: 'http://localhost:3000', // Local environment
                headless: true,
            },
        },
        {
            name: 'safari',
            use: {
                browserName: 'webkit',
                baseURL: 'http://localhost:3000', // Local environment
                headless: true,
            },
        },
    ],
});
