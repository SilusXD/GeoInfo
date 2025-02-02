using GeoInfo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GeoInfo.View
{
    /// <summary>
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        private Users user;
        private Country? countryInfo;
        private City? cityInfo;
        public UserPage(Users user)
        {
            InitializeComponent();

            if (user != null)
            {
                this.user = user;
            }
        }

        private async Task<bool> GetGeoInfo()
        {
            var client = new HttpClient();
            string URL = "http://api.sypexgeo.net/";
            string IP = "";
            try
            {
                IP = await client.GetStringAsync("https://api.ipify.org");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            countryInfo = await GetCountryInfo(client, URL + "json/" + IP);
            cityInfo = await GetCityInfo(client, URL + "json/" + IP);


            if (!countryInfo.HasValue)
            {
                return false;
            }

            if (!cityInfo.HasValue)
            {
                return false;
            }

            txtBlockCountryName.Text = countryInfo.Value.name_ru;
            txtBlockCountryLat.Text = countryInfo.Value.lat.ToString();
            txtBlockCountryLon.Text = countryInfo.Value.lon.ToString();
            txtBlockCountryPopulation.Text = countryInfo.Value.population.ToString();
            txtBlockCountryArea.Text = countryInfo.Value.area.ToString();

            txtBlockCityName.Text = cityInfo.Value.name_ru;
            txtBlockCityLat.Text = cityInfo.Value.lat.ToString();
            txtBlockCityLon.Text = cityInfo.Value.lon.ToString();
            txtBlockCityPopulation.Text = cityInfo.Value.population.ToString();

            return true;
        }
        static async Task<City?> GetCityInfo(HttpClient client, string url)
        {
            City? city = null;
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string jsonTextResult = await response.Content.ReadAsStringAsync();

                    JObject jsonObjectResult = JObject.Parse(jsonTextResult);
                    city = jsonObjectResult["city"].ToObject<City>();
                }
                else
                {
                    Console.WriteLine($"Ошибка: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return city;
        }
        static async Task<Country?> GetCountryInfo(HttpClient client, string url)
        {
            Country? country = null;
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string jsonTextResult = await response.Content.ReadAsStringAsync();

                    JObject jsonObjectResult = JObject.Parse(jsonTextResult);
                    country = jsonObjectResult["country"].ToObject<Country>();
                }
                else
                {
                    Console.WriteLine($"Ошибка: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return country;
        }

        private async void btnGetInfo_Click(object sender, RoutedEventArgs e)
        {
            await GetGeoInfo();
        }

        public bool CreateInfo(Country? countryInfo, City? cityInfo)
        {
            if (countryInfo == null || cityInfo == null)
            {
                MessageBox.Show("Нет данных для записи!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            string countryName = countryInfo.Value.name_ru;
            if (GeoInfoEE.GetContext().Countries.Where(x => x.Country == countryName).Count() < 1)
            {
                Countries tempCountry = new Countries();
                tempCountry.Country = countryName;
                GeoInfoEE.GetContext().Countries.Add(tempCountry);
                GeoInfoEE.GetContext().SaveChanges();
            }

            string cityName = cityInfo.Value.name_ru;
            if (GeoInfoEE.GetContext().Cities.Where(x => x.City == cityName).Count() < 1)
            {
                Cities tempCity = new Cities();
                tempCity.City = cityName;
                GeoInfoEE.GetContext().Cities.Add(tempCity);
                GeoInfoEE.GetContext().SaveChanges();
            }

            CountriesInfo countriesInfo = new CountriesInfo();
            countriesInfo.CountryID = GeoInfoEE.GetContext().Countries.Where(x => x.Country == countryName).FirstOrDefault().ID;
            countriesInfo.Datetime = DateTime.Now;
            countriesInfo.Lat = countryInfo.Value.lat;
            countriesInfo.Lon = countryInfo.Value.lon;
            countriesInfo.Population = countryInfo.Value.population;
            countriesInfo.Area = countryInfo.Value.area;
            countriesInfo.UserID = user.ID;


            CitiesInfo citiesInfo = new CitiesInfo();
            citiesInfo.CityID = GeoInfoEE.GetContext().Cities.Where(x => x.City == cityName).FirstOrDefault().ID;
            citiesInfo.Datetime = DateTime.Now;
            citiesInfo.Lat = cityInfo.Value.lat;
            citiesInfo.Lon = cityInfo.Value.lon;
            citiesInfo.Population = cityInfo.Value.population;
            citiesInfo.UserID = user.ID;

            try
            {
                GeoInfoEE.GetContext().CountriesInfo.Add(countriesInfo);
                GeoInfoEE.GetContext().CitiesInfo.Add(citiesInfo);
                GeoInfoEE.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно сохранены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + '\n' + "Обратитесь к разработчику!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
        private void btnSaveInfo_Click(object sender, RoutedEventArgs e)
        {
            CreateInfo(countryInfo, cityInfo);
        }

        private void btnOpenList_Click(object sender, RoutedEventArgs e)
        {
            new InfoListWindow(user).Show();
        }
    }
}
