using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    public partial class ProductWindow : Window, INotifyPropertyChanged
    {
        BlApi.IBl? bl;
        private BO.Product _product=new BO.Product();

        public event PropertyChangedEventHandler? PropertyChanged;

        public BO.Product product 
        {
            get { return _product; }
            set 
            { 
                _product = value; 
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs (nameof(product)));
            }
        }

        public ProductWindow()
        {
            InitializeComponent();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(Category1));
        }

        public ProductWindow(BO.Product product1)
        {
            InitializeComponent();
            bl = BlApi.Factory.Get();
            product=new BO.Product();
            product=product1;
            int cat = (int)product1.Category!;
            CategorySelector.ItemsSource = Enum.GetValues(typeof(Category1));
            CategorySelector.SelectedIndex = cat;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                bl?.Product.Add(product);
                product = new BO.Product();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Close();
            }

        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl?.Product.Update(product);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Close();
            }
        }

      
    }
}
