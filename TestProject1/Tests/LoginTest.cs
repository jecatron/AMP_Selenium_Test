
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumProject.Pages;
using OpenQA.Selenium.Chrome;
using System;
using TestProject1;
using AventStack.ExtentReports;

namespace SeleniumProject.Tests
{

    [TestFixture]
    public class GoogleTests : TestProject1.TestBase
    {
        private DriverSetup? _driverSetup;
        private IWebDriver? Driver;
        private TestPage1? _testPage;
        public class LoginTest
        {
            private DriverSetup? _driverSetup;
            private IWebDriver? Driver;
            private TestPage1? _testPage;


            [SetUp]
            public void Setup()
            {
                _driverSetup = new DriverSetup();
                Driver = _driverSetup.Driver;
                Driver.Navigate().GoToUrl("file:///C:/Users/josep/Desktop/Demo/Welcomepage.html");

                _testPage = new TestPage1(Driver);
            }

            [Test]
            public void TestLogin()
            {
                // Use encapsulated method to perform login
                _testPage.PerformLogin("admin", "password123");

                // Validate the login was successful
                string actualMessage = _testPage.GetSuccessMessage();
                Assert.That(actualMessage, Is.EqualTo("Login successful!"));
            }

            [TearDown]
            public void Teardown()
            {
                _driverSetup.CloseDriver();
            }
        }
    }
}
