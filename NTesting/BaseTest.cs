using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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

        [SetUp]
        public void setup()
        {
            driver = new ChromeDriver(Directory.GetCurrentDirectory());
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
        }


        [TearDown]
        public void teardown()
        {
            driver.Quit();
        }

    }
}
