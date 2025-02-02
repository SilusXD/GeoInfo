using GeoInfo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace GeoInfo.View
{
    /// <summary>
    /// Логика взаимодействия для EditCountriesInfoPage.xaml
    /// </summary>
    public partial class EditCountriesInfoPage : Page
    {
        private CountriesInfo countriesInfo = new CountriesInfo();
        public EditCountriesInfoPage(CountriesInfo countriesInfo)
        {
            InitializeComponent();

            if(countriesInfo != null)
            {
                this.countriesInfo = countriesInfo;
            }

            cmbCountries.ItemsSource = GeoInfoEE.GetContext().Countries.Select(x => x.Country).ToList();
            cmbUser.ItemsSource = GeoInfoEE.GetContext().Users.Select(x => x.Login).ToList();

            cmbCountries.SelectedIndex = GeoInfoEE.GetContext().Countries.Where(x => x.ID == countriesInfo.CountryID).FirstOrDefault().ID - 1;
            txtBoxDatetime.Text = countriesInfo.Datetime.ToString();
            txtBoxLat.Text = countriesInfo.Lat.ToString();
            txtBoxLon.Text = countriesInfo.Lon.ToString();
            txtBoxPopulation.Text = countriesInfo.Population.ToString();
            txtBoxArea.Text = countriesInfo.Area.ToString();
            cmbUser.SelectedIndex = GeoInfoEE.GetContext().Users.Where(x => x.ID == countriesInfo.UserID).FirstOrDefault().ID - 1;
            this.countriesInfo = countriesInfo;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if(NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        public bool UpdateInfo(string country, string user, DateTime dateTime, double lat, double lon, int population, int area)
        {
            if (string.IsNullOrEmpty(country) || string.IsNullOrEmpty(user) || dateTime == null ||
                lat < 0.0 || lon < 0.0 || population < 0)
            {
                MessageBox.Show("Заполните все поля!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            try
            {
                countriesInfo.CountryID = GeoInfoEE.GetContext().Countries.Where(x => x.Country == country).FirstOrDefault().ID;
                countriesInfo.Datetime = dateTime;
                countriesInfo.Lat = lat;
                countriesInfo.Lon = lon;
                countriesInfo.Population = population;
                countriesInfo.Area = area;
                countriesInfo.UserID = GeoInfoEE.GetContext().Users.Where(x => x.Login == user).FirstOrDefault().ID;

                GeoInfoEE.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно обнолвены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            DateTime.TryParse(txtBoxDatetime.Text, out DateTime dateTime);
            double.TryParse(txtBoxLat.Text, out double lat);
            double.TryParse(txtBoxLon.Text, out double lon);
            int.TryParse(txtBoxPopulation.Text, out int population);
            int.TryParse(txtBoxArea.Text, out int area);

            if(UpdateInfo(cmbCountries.Text, cmbUser.Text, dateTime, lat, lon, population, area))
            {
                if (NavigationService.CanGoBack)
                {
                    NavigationService.GoBack();
                }
            }
        }
    }
}
