using Jewelery.Classes;
using Jewelery.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Jewelery.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductList.xaml
    /// </summary>
    public partial class ProductList : Page
    {

        public ProductList()
        {
            InitializeComponent();
            Manager.Title.Content = "Список товаров";
            Manager.BtnBack.Visibility = Visibility.Visible;
            if(Manager.Role == 2)
            {
                btnAdd.Visibility = Visibility.Collapsed;
                btnDelete.Visibility = Visibility.Collapsed;
            }
            else
            {
                btnAdd.Visibility = Visibility.Visible;
                btnDelete.Visibility = Visibility.Visible;
            }
            btnUb.IsChecked = true;
            cmbSale.SelectedIndex = 0;
            Update();


        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.mainFrame.Navigate(new ProductAddEdit());
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var item = productList.SelectedItems.Cast<VW_ProductList>().FirstOrDefault();
            var products = ContextManager.GetContext().Продукция.ToList();
            ContextManager.GetContext().Продукция.RemoveRange(products.Where(x => x.Артикул == item.Артикул));
            ContextManager.GetContext().SaveChanges();
            Update();
        }

        private void btnUb_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void cmbSale_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        public void Update()
        {
            var products = ContextManager.GetContext().VW_ProductList.ToList();
            if (btnUb.IsChecked == true)
            {
                products = products.OrderBy(x => x.Стоимость).ToList();
            }
            else
            {
                products = products.OrderByDescending(x => x.Стоимость).ToList();
            }

            products = products.Where(x => x.Наименование.ToLower().Contains(txtProductName.Text.ToLower())).ToList();

            if(cmbSale.SelectedIndex != 0)
            {
                switch(cmbSale.SelectedIndex)
                {
                    case 1:
                        products = products.Where(x => x.Действующая_скидка < 10).ToList();
                        break;
                    case 2:
                        products = products.Where(x => x.Действующая_скидка >= 10 && x.Действующая_скидка < 15).ToList();
                        break;

                    case 3:
                        products = products.Where(x => x.Действующая_скидка >= 15).ToList();
                        break;
                }
            }
            productList.ItemsSource = products;

            if (Order.OrderList.Count != 0)
            {
                btnOrder.Visibility = Visibility.Visible;
            }
            else
            {
                btnOrder.Visibility = Visibility.Collapsed;
            }
        }

        private void txtProductName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }



        private void menuAddOrder_Click(object sender, RoutedEventArgs e)
        {
            
            Order.OrderList.Add(productList.SelectedItem as VW_ProductList);
            Update();
        }

        private void menuEdit_Click_1(object sender, RoutedEventArgs e)
        {
            var i = productList.SelectedItem as VW_ProductList;
            Manager.mainFrame.Navigate(new ProductAddEdit(i));

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            new OrderWindow().Show();
        }
    }
}
