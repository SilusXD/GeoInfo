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
    /// Логика взаимодействия для InfoListsPage.xaml
    /// </summary>
    public partial class InfoListsPage : Page
    {
        public InfoListsPage()
        {
            InitializeComponent();

            if(InfoListWindow.User != null)
            {
                listViewCountriesInfo.ItemsSource = GeoInfoEE.GetContext().CountriesInfo.Where(x => x.UserID == InfoListWindow.User.ID).ToList();
                listViewCitiesInfo.ItemsSource = GeoInfoEE.GetContext().CitiesInfo.Where(x => x.UserID == InfoListWindow.User.ID).ToList();
            }
        }

        public bool DeleteInfo(List<CountriesInfo> countriesForRemoving, List<CitiesInfo> citiesForRemoving)
        {
            if(countriesForRemoving.Count == 0 || countriesForRemoving == null || citiesForRemoving.Count == 0 || citiesForRemoving == null) 
            {
                MessageBox.Show("Данные не выбраны!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }
            try
            {
                GeoInfoEE.GetContext().CountriesInfo.RemoveRange(countriesForRemoving);
                GeoInfoEE.GetContext().CitiesInfo.RemoveRange(citiesForRemoving);
                GeoInfoEE.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно удалены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var countriesForRemoving = listViewCountriesInfo.SelectedItems.Cast<CountriesInfo>().ToList();
            var citiesForRemoving = listViewCitiesInfo.SelectedItems.Cast<CitiesInfo>().ToList();

            if (MessageBox.Show($"Удалить {countriesForRemoving.Count() + citiesForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if(DeleteInfo(countriesForRemoving, citiesForRemoving))
                {
                    listViewCountriesInfo.ItemsSource = GeoInfoEE.GetContext().CountriesInfo.ToList();
                    listViewCitiesInfo.ItemsSource = GeoInfoEE.GetContext().CitiesInfo.ToList();
                }
            }
        }

        private void listViewCountriesInfo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new EditCountriesInfoPage(listViewCountriesInfo.SelectedItems.Cast<CountriesInfo>().LastOrDefault()));
        }

        private void listViewCitiesInfo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new EditCitiesInfoPage(listViewCitiesInfo.SelectedItems.Cast<CitiesInfo>().LastOrDefault()));
        }
    }
}
