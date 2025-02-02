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
using System.Windows.Shapes;

namespace GeoInfo.View
{
    /// <summary>
    /// Логика взаимодействия для InfoListWindow.xaml
    /// </summary>
    public partial class InfoListWindow : Window
    {
        public static Users User { get; private set; }
        public InfoListWindow(Users user)
        {
            InitializeComponent();

            if(user != null)
            {
                User = user;
            }
        }
    }
}
