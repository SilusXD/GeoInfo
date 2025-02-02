using GeoInfo.Model;
using GeoInfo.View;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace UnitTestProject1
{
    [TestClass]
    public class Tests4IEditCountriesInfoPageUpdateInfo
    {
        private EditCountriesInfoPage _editCountriesInfoPage;
        private CountriesInfo countriesInfo;

        [TestInitialize]
        public void Initialize()
        {
            countriesInfo = GeoInfoEE.GetContext().CountriesInfo.Where(x => x.Countries.Country == "test").FirstOrDefault();
            _editCountriesInfoPage = new EditCountriesInfoPage(countriesInfo);
        }

        [TestMethod]
        public void UpdateInfo_BothNonEmptyArgs_ReturnsFalse()
        {
            string country = "test";
            string name = "test";
            DateTime dateTime = DateTime.Now;
            double lat = 15.2;
            double lon = 15.3;
            int population = 150000;
            int area = 300000;

            var result = _editCountriesInfoPage.UpdateInfo(country, name, dateTime, lat, lon, population, area);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpdateInfo_EmptyCountryAndName_ReturnsFalse()
        {
            string country = string.Empty;
            string name = string.Empty;
            DateTime dateTime = DateTime.Now;
            double lat = 15.2;
            double lon = 15.3;
            int population = 150000;
            int area = 300000;

            var result = _editCountriesInfoPage.UpdateInfo(country, name, dateTime, lat, lon, population, area);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UpdateInfo_NullCountryAndNullName_ReturnsFalse()
        {
            string country = null;
            string name = null;
            DateTime dateTime = DateTime.Now;
            double lat = 15.2;
            double lon = 15.3;
            int population = 150000;
            int area = 300000;

            var result = _editCountriesInfoPage.UpdateInfo(country, name, dateTime, lat, lon, population, area);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UpdateInfo_EmptyCountryAndNullName_ReturnsFalse()
        {
            string country = string.Empty;
            string name = null;
            DateTime dateTime = DateTime.Now;
            double lat = 15.2;
            double lon = 15.3;
            int population = 150000;
            int area = 300000;

            var result = _editCountriesInfoPage.UpdateInfo(country, name, dateTime, lat, lon, population, area);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UpdateInfo_NullCountryAndEmptyName_ReturnsFalse()
        {
            string country = null;
            string name = string.Empty;
            DateTime dateTime = DateTime.Now;
            double lat = 15.2;
            double lon = 15.3;
            int population = 150000;
            int area = 300000;

            var result = _editCountriesInfoPage.UpdateInfo(country, name, dateTime, lat, lon, population, area);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UpdateInfo_ValidCountryAndNullName_ReturnsFalse()
        {
            string country = "test";
            string name = null;
            DateTime dateTime = DateTime.Now;
            double lat = 15.2;
            double lon = 15.3;
            int population = 150000;
            int area = 300000;

            var result = _editCountriesInfoPage.UpdateInfo(country, name, dateTime, lat, lon, population, area);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UpdateInfo_NullCountryAndValidName_ReturnsFalse()
        {
            string country = null;
            string name = "test";
            DateTime dateTime = DateTime.Now;
            double lat = 15.2;
            double lon = 15.3;
            int population = 150000;
            int area = 300000;

            var result = _editCountriesInfoPage.UpdateInfo(country, name, dateTime, lat, lon, population, area);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UpdateInfo_InvalidArgs_ReturnsFalse()
        {
            string country = null;
            string name = null;
            DateTime dateTime = DateTime.Now;
            double lat = 0;
            double lon = 0;
            int population = 0;
            int area = 0;

            var result = _editCountriesInfoPage.UpdateInfo(country, name, dateTime, lat, lon, population, area);

            Assert.IsFalse(result);
        }
    }
}
