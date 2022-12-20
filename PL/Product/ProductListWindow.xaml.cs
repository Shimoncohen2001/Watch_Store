using BlApi;
using BlImplementation;
using DalApi;
using BO;
using Dal;
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
            // Each column of all rows get the property of the good product
            foreach (var item in bl.Product.GetProductForLists())
            {
                ProductIDListView.Items.Add(item?.ID);
                ProductNameListView.Items.Add(item?.Name);
                ProductCategoryListView.Items.Add(item?.Category);
                ProductPriceListView.Items.Add(item?.Price);
            }
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // We need to remove all the list because we add every time that we modify the category's filter
            ProductIDListView.Items.Clear();
            ProductNameListView.Items.Clear();
            ProductCategoryListView.Items.Clear();
            ProductPriceListView.Items.Clear();

            Object selectedItem = CategorySelector.SelectedItem;
            if (BO.Category.All == (BO.Category)selectedItem)
            {
                foreach (var item in bl.Product.GetProductForLists())
                {
                    ProductIDListView.Items.Add(item?.ID);
                    ProductNameListView.Items.Add(item?.Name);
                    ProductCategoryListView.Items.Add(item?.Category);
                    ProductPriceListView.Items.Add(item?.Price);
                }
            }
            else
            {
                foreach (var item in bl.Product.GetProductForLists(P => P?.Category == (BO.Category)selectedItem))
                {
                    ProductIDListView.Items.Add(item?.ID);
                    ProductNameListView.Items.Add(item?.Name);
                    ProductCategoryListView.Items.Add(item?.Category);
                    ProductPriceListView.Items.Add(item?.Price);
                }
            }
        }

        private void ProductListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Close();
            ProductWindow product = new ProductWindow();
            product.AddBtn.Visibility= Visibility.Collapsed; // The add button doesn't appear if the admin need update operation
            product.Show();
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