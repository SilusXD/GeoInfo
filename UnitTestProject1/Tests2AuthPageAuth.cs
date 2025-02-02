using GeoInfo.View;
using GeoInfo.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GeoInfo.Models;

namespace UnitTestProject
{
    [TestClass]
    public class Tests2AuthPageAuth //Start after TestsAuthPageReg
    {
        private readonly LoginPage _loginPage = new LoginPage();
        [TestMethod]
        public void Auth_EmptyStrings_ReturnsFalse()
        {
            Assert.IsFalse(_loginPage.Auth("", ""));
        }

        [TestMethod]
        public void Auth_NullValues_ReturnsFalse()
        {
            Assert.IsFalse(_loginPage.Auth(null, null));
        }

        [TestMethod]
        public void Auth_OneEmptyString_ReturnsFalse()
        {
            Assert.IsFalse(_loginPage.Auth("", null));
            Assert.IsFalse(_loginPage.Auth(null, ""));
        }

        [TestMethod]
        public void Auth_OneNonEmptyString_ReturnsFalse()
        {
            Assert.IsFalse(_loginPage.Auth("test", ""));
            Assert.IsFalse(_loginPage.Auth("", "test"));
        }

        [TestMethod]
        public void Auth_OneNullOneNonEmptyString_ReturnsFalse()
        {
            Assert.IsFalse(_loginPage.Auth(null, "test"));
            Assert.IsFalse(_loginPage.Auth("test", null));
        }

        [TestMethod]
        public void Auth_BothNonEmptyStrings_ReturnsTrue()
        {
            Assert.IsTrue(_loginPage.Auth("test", "test"));
        }
    }
}
