using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;

public class DriverSetup
{
    public IWebDriver Driver { get; private set; }

    public DriverSetup()
    {

        // Automatically download and set up the correct version of ChromeDriver
        new DriverManager().SetUpDriver(new ChromeConfig());
        Driver = new ChromeDriver();
        Driver.Manage().Window.Maximize();
    }

    public void CloseDriver()
    {
        Driver.Quit();
    }
}