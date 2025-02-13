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

        var chromeOptions = new ChromeOptions();

        // Enable logging for browser console
        chromeOptions.SetLoggingPreference(LogType.Browser, LogLevel.All);

        Driver = new ChromeDriver();
        Driver.Manage().Window.Maximize();
    }

    public void CloseDriver()
    {
        try
        {
            // Corrected the syntax error by adding parentheses around the condition
            if (Driver != null)
            {
                Driver.Quit(); // Properly quit the driver
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error closing driver: {ex.Message}");
        }
    }
}