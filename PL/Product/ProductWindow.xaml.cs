using BlApi;
using BlImplementation;
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

namespace PL.Product
{
    /// <summary>
    /// Logique d'interaction pour ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        private IBl bl = new Bl();
        BO.Product product = new BO.Product();

        public ProductWindow()
        {
            InitializeComponent();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category1));
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Object selecteditems = CategorySelector.SelectedItem;
            product.Category = (BO.Category?)(BO.Category1)selecteditems; // Categories without All
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bl.Product.Add(product);
            this.Close();
            new ProductListWindow().Show();

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string Id;
            Id=IdTextBox.Text;
            product.ID = Convert.ToInt32(Id);
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            product.Name=NameTextBox.Text;
        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {
            string Price;
            Price = PriceTextBox.Text;
            product.Price = Convert.ToDouble(Price);
        }

        private void TextBox_TextChanged_3(object sender, TextChangedEventArgs e)
        {
            string Amount;
            Amount = AmountTextBox.Text;
            product.InStock = Convert.ToInt32(Amount);
        }


        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            bl.Product.Update(product);
            this.Close();
            new ProductListWindow().Show();
        }
    }
}
