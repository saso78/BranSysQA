using Microsoft.Extensions.Logging;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BranSys.BaseTest
{
    public class TestFixture : IDisposable
    {
        public IWebDriver Driver { get; private set; }
        private WebDriverWait wait;
        public ILogger Logger { get; private set; }
        private readonly Random _random = new Random();

        public TestFixture()
        {
            Driver = new ChromeDriver();
            Driver.Navigate().GoToUrl("https://crusader.bransys.com/");
            Driver.Manage().Window.Maximize();
            wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));

            ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
            {
               
            });
            Logger = loggerFactory.CreateLogger("TestFixture");
        }

        public void WaitForElementToDisappear(By locator)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(locator));
        }

        public bool IsElementPresent(By locator)
        {
            try
            {
                Driver.FindElement(locator);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        public void CheckForAlert(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            try
            {
                Logger.LogInformation("Waiting for alert to be present.");
                wait.Until(ExpectedConditions.AlertIsPresent());
                IAlert alert = driver.SwitchTo().Alert();
                Logger.LogInformation("Alert was found: " + alert.Text);
                alert.Accept();
            }
            catch (WebDriverTimeoutException)
            {
                Logger.LogError("No alert was seen within the specified wait time.");
                Assert.Fail("No alert was seen within the specified wait time.");
            }
        }
        public void SendRandomNumbers(int length, int delayInMilliseconds)
        {
            string randomNumbers = GenerateRandomNumbers(length);
            SendKeys(randomNumbers, delayInMilliseconds);
        }

        private string GenerateRandomNumbers(int length)
        {
            string numbers = "0123456789";
            string result = "";
            for (int i = 0; i < length; i++)
            {
                result += numbers[_random.Next(numbers.Length)];
            }
            return result;
        }

        public void SendKeys(string keys, int delayInMilliseconds)
        {
            Actions action = new Actions(Driver);
            action.SendKeys(keys).Perform();
            Thread.Sleep(delayInMilliseconds);
        }

        public void SendSpecialKey(string key, int delayInMilliseconds)
        {
            Actions action = new Actions(Driver);
            action.SendKeys(key).Perform();
            Thread.Sleep(delayInMilliseconds);
        }
       
       
        
        public void RightClickElement(By locator)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(locator));
            IWebElement element = Driver.FindElement(locator);
            Actions actions = new Actions(Driver);
            actions.ContextClick(element).Perform();
        }
        public void Login(string username, string password)
        {
            IWebElement userNameField = Driver.FindElement(By.Id("input-204"));
            userNameField.SendKeys(username);
            IWebElement userPasswordField = Driver.FindElement(By.Id("input-207"));
            userPasswordField.SendKeys(password);
            IWebElement loginButton = Driver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div/div/div/div/div/div[2]/div[2]/form/div[2]/div[1]/button"));
            loginButton.Click();
            
        }

        public void Dispose()
        {
            Driver.Quit();
        }
    }
}


