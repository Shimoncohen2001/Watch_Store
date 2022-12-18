using BlApi;
using BlImplementation;
using PL.Product;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IBl bl = new Bl();
        //ProductListWindow productListWindow = new ProductListWindow();  

        public MainWindow()
        {
            InitializeComponent();
        }

        //private void AdminButton_Click(object sender, RoutedEventArgs e)
        //{
        //    productListWindow.Show();
        //}

        private void AdminButton_Click(object sender, RoutedEventArgs e) => new ProductListWindow().Show();
    }
}
