using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.chrome;   

namespace SeleniumProject.Pages
{
    public class TestPage1
    {
        private readonly IWebDriver Driver;
        private readonly ObjectRepository _repo;

        private readonly By UsernameField;
        private readonly By PasswordField;
        private readonly By LoginButton;
        private readonly By SuccessfulLogin;

        public TestPage1(IWebDriver driver)
        {
            Driver = driver;
            _repo = new ObjectRepository("C:\\Users\\josep\\source\\repos\\Solution1\\TestProject1\\ObjectRepository\\ObjRepo.xml");

            // Load element locators from XML and assign them to By objects
            UsernameField = _repo.GetByLocator("username");
            PasswordField = _repo.GetByLocator("password");
            LoginButton = _repo.GetByLocator("LoginButton");
            SuccessfulLogin = _repo.GetByLocator("successfulLogin");
        }
        

        // Methods for login actions
        public void EnterUsername(string username)
        {
            Driver.FindElement(UsernameField).SendKeys(username);
        }
        // Method for entering password
        public void EnterPassword(string password)
        {
            Driver.FindElement(PasswordField).SendKeys(password);
        }
        //Method for clicking the login button
        public void ClickLoginButton()
        {
            Driver.FindElement(LoginButton).Click();
        }

        // Method for validation
        public string GetSuccessMessage()
        {
            try
            {
                // Wait for the page to load completely
                IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
                while (js.ExecuteScript("return document.readyState").ToString() != "complete")
                {
                    Thread.Sleep(500);  // Wait for 500 ms before checking again
                }

                // Wait for alert to be present
                IAlert alert = Driver.SwitchTo().Alert();

                // Get and accept the alert
                string alertText = alert.Text;
                alert.Accept();

                return alertText;
            }
            catch (NoAlertPresentException)
            {
                // Return empty string if no alert is found
                return string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🚨 EXCEPTION in GetSuccessMessage(): {ex.Message}");
                return string.Empty;
            }
        }




        // Encapsulated method to perform full login
        public void PerformLogin(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            ClickLoginButton();
        }
    }
}
