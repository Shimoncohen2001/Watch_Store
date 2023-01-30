using BO;
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

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for OrderTrackingWindow.xaml
    /// </summary>
    public partial class OrderTrackingWindow : Window
    {
        public OrderTracking OrderTracking = new();

        BlApi.IBl? bl;
        public OrderTrackingWindow(string text)
        {
            InitializeComponent();
            bl = BlApi.Factory.Get();
            OrderTracking = bl?.Order.TrackingOrder(Convert.ToInt32(text))!;
        }

        private void OrderDetailsButton_Click(object sender, RoutedEventArgs e) { } //=> new OrderWindow().Show();
        
    }
}
