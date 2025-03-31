using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WpfAppBaev;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest4
    {
        [TestMethod]
        public void RegTestSuccess()
        {
            var page = new SingUpPage();
            Assert.IsTrue(page.Register("Иван", "Иванов", "Men1", "ivanivanov@gmail.com", "+7(903)456-67-34", "qwerty123"));
            Assert.IsFalse(page.Register("Петр", "Петров", "Men1", "petrovv@gmail.com", "+7(903)676-37-23", "qwerty12"));
            Assert.IsFalse(page.Register("Николай", "Николаев", "Men123", "nikolaev@gmail.com", "+7(903)667-33-48", "выаы"));
            Assert.IsFalse(page.Register("", "Петров", "Menwer34", "petrov@gmail.com", "+7(453)456-34-12", "qwertеy12"));
            Assert.IsFalse(page.Register("Алексей", "Алексеев", "Alex1", "allgmail.com", "+7(453)456-34-12", "qwertеy12"));
            Assert.IsFalse(page.Register("Алексей", "Алексеев", "Al56", "allex@gmail.com", "+7(453)676-34-12", "qwe1"));
            Assert.IsFalse(page.Register("Алексей", "Алексеев", "Alex789", "ally@gmail.com", "74534563412", "qwerty1234"));
        }
    }
}