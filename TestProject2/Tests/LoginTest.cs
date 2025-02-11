using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumProject.Pages;

namespace SeleniumProject.Tests
{
    public class LoginTest
    {
        private DriverSetup _driverSetup;
        private IWebDriver Driver;
        private TestPage1 _testPage;
        //private ChromeDriver chromeDriver

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
            Assert.AreEqual("Login Successful!", actualMessage);
        }

        [TearDown]
        public void Teardown()
        {
            _driverSetup.CloseDriver();
        }
    }
}
