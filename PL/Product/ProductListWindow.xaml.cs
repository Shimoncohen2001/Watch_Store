using BlApi;
using BlImplementation;
using BO;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
            ProductListView.ItemsSource = bl.Product.GetProductForLists();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Object selectedItem = CategorySelector.SelectedItem;
            if (BO.Category.Null == (BO.Category)selectedItem)
            {
                ProductListView.ItemsSource = bl.Product.GetProductForLists();
            }
            else
                ProductListView.ItemsSource = bl.Product.GetProductForLists(P => P?.Category == (BO.Category)selectedItem);
        }

        private void ProductListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Close();
            ProductWindow product = new ProductWindow();
            product.AddBtn.Visibility= Visibility.Collapsed;
            product.Show();
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            ProductWindow product = new ProductWindow();
            product.UpdateBtn.Visibility = Visibility.Collapsed;
            product.Show();
        }
      
    }
}