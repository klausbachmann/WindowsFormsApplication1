using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTesting.Navigation
{
    [TestFixture]
    class Navigation : BaseTest
    {
        [Test]
        public void Method3()
        {

            driver.Url = "http://www.tuicruises.com";
            //Assert.Equals("Kreuzfahrten finden",
            //  driver.FindElement(By.CssSelector("# nav > ul > li:nth-child(4) > a > span")).Text);
        }


        [Test]
        public void Method1()
        {
            driver = new ChromeDriver(Directory.GetCurrentDirectory());
            driver.Url = "http://www.pro7.de";
        }

        [Test]
        public void Method2() { }

        [Test]
        public void Method4() { }
        [Test]
        public void Method5() { }
        [Test]
        public void Method6() { }

        [Test]
        public void PowerMethod5000() { }

    }
}
