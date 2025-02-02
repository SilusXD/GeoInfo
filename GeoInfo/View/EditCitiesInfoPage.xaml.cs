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
    /// Логика взаимодействия для EditCitiesInfoPage.xaml
    /// </summary>
    public partial class EditCitiesInfoPage : Page
    {
        private CitiesInfo citiesInfo = new CitiesInfo();
        public EditCitiesInfoPage(CitiesInfo citiesInfo)
        {
            InitializeComponent();

            if (citiesInfo != null)
            {
                this.citiesInfo = citiesInfo;
            }

            cmbCities.ItemsSource = GeoInfoEE.GetContext().Cities.Select(x => x.City).ToList();
            cmbUser.ItemsSource = GeoInfoEE.GetContext().Users.Select(x => x.Login).ToList();

            cmbCities.SelectedIndex = GeoInfoEE.GetContext().Cities.Where(x => x.ID == citiesInfo.CityID).FirstOrDefault().ID - 1;
            txtBoxDatetime.Text = citiesInfo.Datetime.ToString();
            txtBoxLat.Text = citiesInfo.Lat.ToString();
            txtBoxLon.Text = citiesInfo.Lon.ToString();
            txtBoxPopulation.Text = citiesInfo.Population.ToString();
            cmbUser.SelectedIndex = GeoInfoEE.GetContext().Users.Where(x => x.ID == citiesInfo.UserID).FirstOrDefault().ID - 1;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        public bool UpdateInfo(string city, string user, DateTime dateTime, double lat, double lon, int population)
        {
            if (string.IsNullOrEmpty(city) || string.IsNullOrEmpty(user) || dateTime == null ||
                lat < 0.0 || lon < 0.0 || population < 0)
            {
                MessageBox.Show("Заполните все поля!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            try
            {
                citiesInfo.CityID = GeoInfoEE.GetContext().Cities.Where(x => x.City == city).FirstOrDefault().ID;
                citiesInfo.Datetime = dateTime;
                citiesInfo.Lat = lat;
                citiesInfo.Lon = lon;
                citiesInfo.Population = population;
                citiesInfo.UserID = GeoInfoEE.GetContext().Users.Where(x => x.Login == user).FirstOrDefault().ID;

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

            if(UpdateInfo(cmbCities.Text, cmbUser.Text, dateTime, lat, lon, population))
            {
                if (NavigationService.CanGoBack)
                {
                    NavigationService.GoBack();
                }
            }
        }
    }
}
