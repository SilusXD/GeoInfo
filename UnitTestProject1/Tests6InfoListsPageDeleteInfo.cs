using GeoInfo.Model;
using GeoInfo.View;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestProject1
{
    [TestClass]
    public class Tests6InfoListsPageDeleteInfo
    {
        private InfoListsPage _infoListsPage = new InfoListsPage();

        [TestMethod]
        public void DeleteInfo_EmptyArgs_ReturnsFalse()
        {
            var listCountriesInfo = new List<CountriesInfo>();
            var listCitiesInfo = new List<CitiesInfo>();

            var result = _infoListsPage.DeleteInfo(listCountriesInfo, listCitiesInfo);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DeleteInfo_NonEmptyArgs_ReturnsTrue()
        {
            var countriesInfo = GeoInfoEE.GetContext().CountriesInfo.Where(x => x.Countries.Country == "test").ToList();
            var citiesInfo = GeoInfoEE.GetContext().CitiesInfo.Where(x => x.Cities.City == "test").ToList();

            var result = _infoListsPage.DeleteInfo(countriesInfo, citiesInfo);

            Assert.IsTrue(result);
        }
    }
}
