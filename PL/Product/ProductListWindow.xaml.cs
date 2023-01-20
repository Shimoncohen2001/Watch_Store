
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
using System.ComponentModel;
using System.Runtime.CompilerServices;

/// <summary>
/// Logique of interaction for ProductListWindow.xaml
/// </summary>
namespace PL.Product
{
    public partial class ProductListWindow : Window, INotifyPropertyChanged
    {
        BlApi.IBl? bl;
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private List<ProductForList> _ProductForLists;
        public List<ProductForList> ProductForLists
        {
            get { return _ProductForLists; }
            set
            {
                _ProductForLists = value;
                NotifyPropertyChanged();
            }
        }
        public ProductListWindow()
        {
            InitializeComponent();
            bl = BlApi.Factory.Get();
            ProductForLists = new List<ProductForList>(bl?.Product?.GetProductForLists());
            lstView.ItemsSource = ProductForLists;
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
            ////this.Close();
            //ProductWindow product = new ProductWindow();
            //product.UpdateBtn.Visibility = Visibility.Collapsed; // The update button doesn't appear if the admin need add operation
            //product.ShowDialog();
        }

        /// <summary>
        /// Button for to update a product and open productWindow for this
        /// For this we need to click two times on a product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductListWindow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ProductForList productForList1=lstView.SelectedItem as ProductForList;
            BO.Product product1 = new BO.Product();
            product1 = bl?.Product.GetDirector(productForList1.ID);
            ProductWindow product = new ProductWindow(product1);
            product.AddBtn.Visibility = Visibility.Collapsed; // The add button doesn't appear if the admin need update operation
            product.Show();
            ProductForLists = bl?.Product.GetProductForLists().ToList();
            lstView.Items.Refresh();
            lstView.ItemsSource = ProductForLists;
        }



    }
}