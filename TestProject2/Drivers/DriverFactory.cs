using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestProject1.Drivers
{
    public static class DriverFactory
    {
        public static IWebDriver CreateDriver()
        {
            //Set ChromeDriver options
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");

            //Initialize WebDriver
            return new ChromeDriver(options);
        }

    }


}
