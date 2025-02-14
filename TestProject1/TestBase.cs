using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using TestProject1.Reporting;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;

namespace TestProject1
{
    public abstract class TestBase
    {
        protected IWebDriver driver;
        protected ExtentTest test;
        protected ExtentReports extentReports;
        

        [OneTimeSetUp] // This runs once for the entire test suite
        public void GlobalSetup()
        {
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string reportPath = $"C:\\Users\\josep\\source\\repos\\Solution1\\TestProject1\\Reporting\\Reports\\ExtentReport_{timestamp}.html"; // Set the desired path

            // Initialize the ExtentSparkReporter (used to generate the Spark report)
            var sparkReporter = new ExtentSparkReporter(reportPath);

            // Set the title of the report
            sparkReporter.Config.ReportName = "Test Execution Report";
            sparkReporter.Config.DocumentTitle = "Test Results";

            // Initialize the ExtentReports object and attach the Spark reporter
            extentReports = new ExtentReports();
            extentReports.AttachReporter(sparkReporter);
        }


        [SetUp]
        public void Setup()
        {
            // Initialize WebDriver 
            new DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();  // Initialize the global driver property
            driver.Manage().Window.Maximize(); // Maximize the browser window
            


            // Create a new test entry using the global ExtentReports instance
            test = extentReports.CreateTest(TestContext.CurrentContext.Test.Name);

        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                // Log the outcome of the test in ExtentReports
                var status = TestContext.CurrentContext.Result.Outcome.Status;
                if (status == NUnit.Framework.Interfaces.TestStatus.Passed)
                {
                    test.Log(Status.Pass, "Test Passed");
                }
                else if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
                {
                    test.Log(Status.Fail, "Test Failed");
                }


                // Log the browser console logs
                // Capture and log browser logs
                LogBrowserConsoleLogs(); 

                // Always quit the driver
                if (driver != null)
                {
                    driver.Quit();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error closing driver: {ex.Message}");
            }
            
        }

        // This runs once after the entire test suite
        [OneTimeTearDown] 
        public void GlobalTearDown()
        {
            // Flush and close the report after all tests are completed
            extentReports.Flush();


        }
        private void LogBrowserConsoleLogs()
        {
            var logs = driver.Manage().Logs.GetLog(LogType.Browser);
            foreach (var log in logs)
            {
                test.Log(Status.Info, "Browser Log: " + log.Message);
            }
        }
    }

}
