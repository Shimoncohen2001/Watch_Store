
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        BlApi.IBl? bl = BlApi.Factory.Get();
        BO.Product product = new BO.Product();

        public ProductWindow()
        {
            InitializeComponent();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category1));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                product.ID = int.Parse(this.IdTextBox.Text);
                Object selecteditems = CategorySelector.SelectedItem;
                product.Category = (BO.Category?)(BO.Category1)selecteditems; // Categories without All
                product.Name = NameTextBox.Text;
                product.Price = double.Parse(this.PriceTextBox.Text);
                product.InStock = int.Parse(this.AmountTextBox.Text);
                bl?.Product.Add(product);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Close();
                new ProductListWindow().Show();
            }

        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            bl?.Product.Update(product);
            this.Close();
            new ProductListWindow().Show();
        }
    }
}
