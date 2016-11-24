using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTesting
{
    [TestFixture]
    class Basictest
    {
        [Test]
        public void Test1()
        {
            Assert.Equals(1, 0);
        }

        [Test]
        public void Test2()
        {
            Assert.Fail("Object nicht gefunden");
        }

        [Test]
        public void Test3() { }

        [Test]
        public void Test4() { }

        [Test]
        public void Test5() { }

        [Test]
        public void Test6() { }
    }
}
