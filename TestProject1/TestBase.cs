using AventStack.ExtentReports;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using TestProject1.Reporting;

namespace TestProject1
{
    public abstract class TestBase
    {
        protected IWebDriver driver;
        protected ExtentTest test;

        [SetUp]
        public void Setup()
        {
            // Initialize WebDriver (modify as needed for your tests)
            driver = new ChromeDriver();

            // Create a new test entry using the global ExtentReports instance
            test = ExtentReportSetup.Extent.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                // Log test result based on the current test outcome
                var status = TestContext.CurrentContext.Result.Outcome.Status;
                switch (status)
                {
                    case NUnit.Framework.Interfaces.TestStatus.Failed:
                        test.Log(Status.Fail, "Test failed");
                        break;
                    case NUnit.Framework.Interfaces.TestStatus.Passed:
                        test.Log(Status.Pass, "Test passed");
                        break;
                    default:
                        test.Log(Status.Warning, "Test had an unexpected status");
                        break;
                }
            }
            finally
            {
                // Always quit the driver even if logging fails
                driver.Quit();
            }
        }
    }
}
