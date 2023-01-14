
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
using System.Collections.ObjectModel;

/// <summary>
/// Logique of interaction for ProductListWindow.xaml
/// </summary>
namespace PL.Product
{
    public partial class ProductListWindow : Window
    {
        BlApi.IBl? bl;

        public ProductListWindow()
        {
            InitializeComponent();
            bl = BlApi.Factory.Get();
            List<ProductForList>productForLists = new List<ProductForList>();
            productForLists = bl?.Product.GetProductForLists().ToList()!;
            lstView.DataContext = productForLists;
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
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

        /// <summary>
        /// Button for to add a product and open productWindow for this
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            //this.Close();
            ProductWindow product = new ProductWindow();
            product.UpdateBtn.Visibility = Visibility.Collapsed; // The update button doesn't appear if the admin need add operation
            product.ShowDialog();
            lstView.DataContext = bl?.Product.GetProductForLists()!;
        }

        /// <summary>
        /// Button for to update a product and open productWindow for this
        /// For this we need to click two times on a product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductListWindow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ProductWindow product = new ProductWindow();
            product.AddBtn.Visibility = Visibility.Collapsed; // The add button doesn't appear if the admin need update operation
            product.ShowDialog();
            lstView.DataContext = bl?.Product.GetProductForLists()!;
        }
    }
}