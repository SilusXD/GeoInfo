using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeoInfo.Models;
using System;
using GeoInfo.Model;
using System.Linq;

namespace UnitTestProject1
{
    [TestClass]
    public class Tests1AuthPageReg
    {
        private readonly LoginPage _loginPage = new LoginPage();

        [TestMethod]
        public void Reg_EmptyStrings_ReturnsFalse()
        {
            Assert.IsFalse(_loginPage.Reg("", ""));
            GeoInfoEE.GetContext().Cities.ToList();
        }

        [TestMethod]
        public void Reg_NullValues_ReturnsFalse()
        {
            Assert.IsFalse(_loginPage.Reg(null, null));
        }

        [TestMethod]
        public void Reg_OneEmptyString_ReturnsFalse()
        {
            Assert.IsFalse(_loginPage.Reg("", null));
            Assert.IsFalse(_loginPage.Reg(null, ""));
        }

        [TestMethod]
        public void Reg_OneNonEmptyString_ReturnsFalse()
        {
            Assert.IsFalse(_loginPage.Reg("test", ""));
            Assert.IsFalse(_loginPage.Reg("", "test"));
        }

        [TestMethod]
        public void Reg_OneNullOneNonEmptyString_ReturnsFalse()
        {
            Assert.IsFalse(_loginPage.Reg(null, "test"));
            Assert.IsFalse(_loginPage.Reg("test", null));
        }

        [TestMethod]
        public void Reg_BothNonEmptyStrings_ReturnsTrue()
        {
            Assert.IsTrue(_loginPage.Reg("test", "test"));
            Assert.IsFalse(_loginPage.Reg("test", "test"));
        }


    }
}
