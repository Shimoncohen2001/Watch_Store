
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
using System.Collections.Specialized;

/// <summary>
/// Logique of interaction for ProductListWindow.xaml
/// </summary>
namespace PL.Product
{
    public partial class ProductListWindow : Window, INotifyPropertyChanged
    {
        BlApi.IBl? bl;
        BO.Product Productupdated { get; set; }
        private ObservableCollection<BO.ProductForList?> _ProductForLists;
        public ObservableCollection<BO.ProductForList?> ProductForLists
        {
            get { return _ProductForLists; }

            set
            {
                _ProductForLists = new ObservableCollection<ProductForList?>(bl?.Product.GetProductForLists());
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        //public event EventHandler? updateProductForLists;

        //private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        //public List<ProductForList> ProductForListss
        //{
        //    get { return _ProductForLists; }
        //    set
        //    {
        //        _ProductForLists = bl?.Product?.GetProductForLists().ToList()!;
        //        NotifyPropertyChanged();
        //    }
        //}
        public ProductListWindow()
        {
            InitializeComponent();
            bl = BlApi.Factory.Get();
            ProductForLists= new ObservableCollection<BO.ProductForList?>(bl?.Product?.GetProductForLists()!);
            //lstView.ItemsSource = ProductForLists;// sans definir le dataconxte de la fennetre comme etant elle meme le items source ne peux pas marcher
            //DataContext = this;// le but est de definir le datacontexte a une source de donne sauf que la source de donne d'une fennetre est la fennetre elle meme
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }



        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Object selectedItem = CategorySelector.SelectedItem;
            if (BO.Category.All == (BO.Category)selectedItem)
            {
                //ProductForLists = new List<ProductForList>(bl?.Product.GetProductForLists()!);
                //updateProductForLists += ProductListWindow_updateProductForLists;
                ProductForLists = new ObservableCollection<BO.ProductForList?>(bl?.Product?.GetProductForLists()!);
                //lstView.ItemsSource= ProductForLists;
            }
            else
            {
                lstView.ItemsSource = bl?.Product.GetProductForLists(P => P?.Category == (BO.Category)selectedItem);
            }
        }

        private void ProductListWindow_updateProductForLists(object? sender, EventArgs e)
        {
            lstView.ItemsSource = bl?.Product?.GetProductForLists().ToList();
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
            ProductForList productForList1 = lstView.SelectedItem as ProductForList;
            Productupdated = new BO.Product();
            Productupdated = bl?.Product.GetDirector(productForList1.ID);
            //////
            ProductWindow product = new ProductWindow(Productupdated);
            product.AddBtn.Visibility = Visibility.Collapsed; // The add button doesn't appear if the admin need update operation
            product.Show();
            lstView.ItemsSource = bl?.Product.GetProductForLists();
            //lstView.DataContext=ProductForLists;
        }



    }
}