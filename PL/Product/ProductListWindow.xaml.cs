
using DalApi;
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
        BlApi.IBl? bl = BlApi.Factory.Get();
        public ProductListWindow()
        {
            InitializeComponent();
            List<BO.ProductForList>productForLists = new List<BO.ProductForList>();
            productForLists = bl?.Product.GetProductForLists().ToList();
            lstView.ItemsSource = productForLists;
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }

        private void ProductListWindow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ProductWindow product = new ProductWindow();
            product.AddBtn.Visibility = Visibility.Collapsed; // The add button doesn't appear if the admin need update operation
            product.Show();
        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Object selectedItem = CategorySelector.SelectedItem;
            if (BO.Category.All == (BO.Category)selectedItem)
            {
                lstView.ItemsSource = bl?.Product.GetProductForLists();
            }
            else
            {
                lstView.ItemsSource = bl?.Product.GetProductForLists(P => P?.Category == (BO.Category)selectedItem);
            }
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            ProductWindow product = new ProductWindow();
            product.UpdateBtn.Visibility = Visibility.Collapsed; // The update button doesn't appear if the admin need add operation
            product.Show();
        }

        
        
    }
}