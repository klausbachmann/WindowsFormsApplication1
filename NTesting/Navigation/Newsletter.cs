using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTesting.Navigation
{
    [TestFixture]
    class Newsletter : BaseTest
    {
        [Test]
        public void simpleExtendTest()
        {
            driver.Url = "http://www.rtl.de";

        }

    }
}
