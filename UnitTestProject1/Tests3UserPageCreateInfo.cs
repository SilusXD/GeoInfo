using GeoInfo.Model;
using GeoInfo.Models;
using GeoInfo.View;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace UnitTestProject1
{
    [TestClass]
    public class Tests3UserPageCreateInfo
    {
        private UserPage _userPage;
        private Users testUser;

        [TestInitialize]
        public void Initialize()
        {
            testUser = GeoInfoEE.GetContext().Users.Where(x => x.Login == "test").FirstOrDefault();
            _userPage = new UserPage(testUser);
        }

        [TestMethod]
        public void CreateInfo_EmptyArgs_ReturnsFalse()
        {
            Country countryInfo = new Country();
            City cityInfo = new City();

            var result = _userPage.CreateInfo(countryInfo, cityInfo);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateInfo_NullValues_ReturnsFalse()
        {
            var result = _userPage.CreateInfo(null, null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateInfo_OneNullOneNonEmpty_ReturnsFalse()
        {
            Country countryInfo = new Country();
            countryInfo.name_ru = "test";
            countryInfo.lat = 10;
            countryInfo.lon = 10;
            countryInfo.population = 10;
            countryInfo.area = 10;

            var result = _userPage.CreateInfo(countryInfo, null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateInfo_OneNonEmptyOneNull_ReturnsFalse()
        {
            City cityInfo = new City();
            cityInfo.name_ru = "test";
            cityInfo.lat = 10;
            cityInfo.lon = 10;
            cityInfo.population = 10;


            var result = _userPage.CreateInfo(null, cityInfo);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateInfo_BothNonEmptyArgs_ReturnsTrue()
        {
            Country countryInfo = new Country();
            countryInfo.name_ru = "test";
            countryInfo.lat = 10;
            countryInfo.lon = 10;
            countryInfo.population = 10;
            countryInfo.area = 10;

            City cityInfo = new City();
            cityInfo.name_ru = "test";
            cityInfo.lat = 10;
            cityInfo.lon = 10;
            cityInfo.population = 10;


            var result = _userPage.CreateInfo(countryInfo, cityInfo);

            Assert.IsTrue(result);
        }
        
    }
}
