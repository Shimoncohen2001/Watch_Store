using BlApi;
using BlImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace PL.Product
{
    /// <summary>
    /// Logique d'interaction pour ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        private IBl bl = new Bl();
        public ProductListWindow()
        {
            InitializeComponent();
        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ShowProductButton_Click(object sender, RoutedEventArgs e) => new ProductListWindow().Show();
    }
}
