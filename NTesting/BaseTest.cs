using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTesting
{
    [TestFixture]
    class BaseTest
    {

        protected IWebDriver driver;
        protected WebDriverWait wait;

        [SetUp]
        public void setup(IWebDriver driver)
        {

            driver = new ChromeDriver(Directory.GetCurrentDirectory());
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }


        [TearDown]
        public void teardown()
        {
            driver.Quit();
        }

    }
}