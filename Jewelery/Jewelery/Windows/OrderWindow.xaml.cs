using Jewelery.Classes;
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

namespace Jewelery.Windows
{
    /// <summary>
    /// Логика взаимодействия для OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        public OrderWindow()
        {
            InitializeComponent();
            productList.ItemsSource = Order.OrderList.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void menuDelete_Click(object sender, RoutedEventArgs e)
        {
            var product = productList.SelectedItem as VW_ProductList;
            Order.OrderList.Remove(product);
            productList.ItemsSource = Order.OrderList.ToList();
        }
    }
}
