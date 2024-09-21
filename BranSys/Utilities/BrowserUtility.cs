using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace BranSys.Utilities
{
    public class TestContext : IDisposable
    {
        public IWebDriver Driver { get; private set; }

        public TestContext()
        {
            Driver = new ChromeDriver();
            Driver.Navigate().GoToUrl("https://crusader.bransys.com/");
            Driver.Manage().Window.Maximize();
        }

        public void Dispose()
        {

        }
    }
}
