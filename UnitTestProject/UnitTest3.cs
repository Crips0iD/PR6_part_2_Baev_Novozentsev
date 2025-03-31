using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WpfAppBaev;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest3
    {
        [TestMethod]
        public void AuthTestSuccess()
        {
            var page = new Login(null);
            Assert.IsTrue(page.Auth("Mam0ru", "qwerty123"));
            Assert.IsTrue(page.Auth("Vicktus", "qwerty123"));
            Assert.IsTrue(page.Auth("Admin", "Admin123"));
        }
    }
}
