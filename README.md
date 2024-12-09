
# **Playwright-RestSharp-Azure: Juice Shop End-to-End Testing Framework**

## **Project Overview**
This repository demonstrates an **end-to-end testing framework** for the OWASP Juice Shop application. The framework includes:

- **UI Testing**: Automated cross-browser testing using **Playwright**.
- **API Testing**: Comprehensive backend API testing using **RestSharp** in **C#**.
- **CI/CD Integration**: Full pipeline setup in **Azure DevOps** for seamless test automation and execution.

This project showcases a robust approach to software testing using industry-standard tools, techniques, and best practices.

---

## **Features**
1. **Frontend/UI Testing**:
   - Cross-browser automation using Playwright.
   - Modular and scalable test scripts for user interactions.
   - Features like retries, screenshots, and HTML reports.
   
2. **Backend/API Testing**:
   - Authentication and API testing using RestSharp.
   - JSON parsing with assertions for functional validation.
   - Modularized base test classes for scalability.

3. **Continuous Integration/Continuous Deployment (CI/CD)**:
   - Automated test execution using Azure DevOps pipelines.
   - Integration of both UI and API tests into a single pipeline.

4. **Secure Configurations**:
   - Sensitive data stored in `.env` or `appsettings.json` files.
   - Secrets managed through Azure Pipeline variables.

---

## **Technologies Used**

| **Tool/Tech**           | **Purpose**                                      |
|--------------------------|--------------------------------------------------|
| **Playwright**           | UI Testing Framework for frontend testing.       |
| **RestSharp**            | HTTP client library for API testing in C#.       |
| **NUnit**                | Testing framework for API tests in .NET.         |
| **Azure DevOps Pipelines** | CI/CD pipeline for automated testing.          |
| **Node.js**              | Playwright runtime.                              |
| **.NET**                 | Platform for backend testing with C#.            |
| **Azure App Services**   | Hosting the Juice Shop application.              |

---

## **Project Setup**

### **1. Juice Shop Application**
1. Clone the Juice Shop repository:
   ```bash
   git clone https://github.com/juice-shop/juice-shop.git
   cd juice-shop
   ```

2. Install dependencies:
   ```bash
   npm install
   ```

3. Run locally to verify functionality:
   ```bash
   npm start
   ```
   - Access the app at `http://localhost:3000`.

4. Optional: Deploy to a free test environment (e.g., Azure App Services or Heroku) for pipeline integration.

---

### **2. UI Testing with Playwright**

#### **Setup**
1. Navigate to `frontend_tests/playwright` and initialize the project:
   ```bash
   npm init -y
   npm install @playwright/test
   npx playwright install
   ```

2. **Directory Structure**:
   ```
   frontend_tests/playwright/
   ├── integration/                # Playwright test scripts
   ├── playwright.config.js        # Configuration file
   ├── test-results/               # Test results
   ├── playwright-report/          # HTML reports
   ├── package.json                # NPM configuration
   ├── .env                        # Environment variables (optional)
   ```

3. Run Playwright tests:
   ```bash
   npx playwright test
   ```

---

### **3. API Testing with RestSharp**

#### **Setup**
1. Navigate to `backend_tests/JuiceShopApiTests` and initialize a .NET NUnit project:
   ```bash
   dotnet new nunit -n JuiceShopApiTests
   cd JuiceShopApiTests
   ```

2. Add dependencies:
   ```bash
   dotnet add package RestSharp
   dotnet add package Newtonsoft.Json
   dotnet add package Microsoft.Extensions.Configuration.Json
   ```

3. **Directory Structure**:
   ```
   backend_tests/JuiceShopApiTests/
   ├── AuthTests.cs               # Tests for login API
   ├── BasketTests.cs             # Tests for basket API
   ├── BaseTest.cs                # Shared setup logic
   ├── appsettings.json           # Configuration for sensitive data
   ├── JuiceShopApiTests.csproj   # Project file
   ```

4. Run API tests:
   ```bash
   dotnet test
   ```

---

### **4. CI/CD with Azure DevOps**

#### **Pipeline Setup**
1. Add an `azure-pipelines.yml` file to the root of your repository:
   ```yaml
   trigger:
     branches:
       include:
         - main

   pool:
     vmImage: 'windows-latest'

   steps:
   # Checkout the code
   - task: Checkout@1
     displayName: 'Checkout Code'

   # Run C# API Tests
   - task: UseDotNet@2
     displayName: 'Install .NET SDK'
     inputs:
       packageType: 'sdk'
       version: '6.x'

   - task: DotNetCoreCLI@2
     displayName: 'Restore Dependencies'
     inputs:
       command: 'restore'
       projects: '**/*.csproj'

   - task: DotNetCoreCLI@2
     displayName: 'Run API Tests'
     inputs:
       command: 'test'
       projects: '**/*Tests.csproj'

   # Run Playwright Tests
   - task: UseNode@2
     displayName: 'Install Node.js'
     inputs:
       version: '16.x'

   - script: npm ci
     displayName: 'Install Playwright Dependencies'
     workingDirectory: frontend_tests/playwright

   - script: npx playwright test
     displayName: 'Run Playwright Tests'
     workingDirectory: frontend_tests/playwright
   ```

2. Commit and push the pipeline configuration to your repository.

3. Set up a pipeline in Azure DevOps and run it.

---

## **Future Improvements**
- Add cross-browser testing with BrowserStack or LambdaTest.
- Integrate performance testing using tools like JMeter or k6.
- Enhance CI/CD with deployment stages.

---

## **Conclusion**
This project highlights an end-to-end testing framework integrating **Playwright, RestSharp, and Azure DevOps pipelines**. It demonstrates robust and scalable testing practices, showcasing proficiency in modern testing tools and cloud-based workflows.
