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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Jewelery.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            Manager.Title.Content = "Авторизация";
            Manager.BtnBack.Visibility = Visibility.Collapsed;
            txtLogin.Text = "loginDEpsu2018";
            txtPassword.Password = "Vx9cQ{";

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtLogin.Text.ToString() != "" && txtPassword.Password.ToString() != "")
            {
                var user = ContextManager.GetContext().Пользователи.Where(x => x.Логин == txtLogin.Text.ToString() && x.Пароль == txtPassword.Password.ToString()).FirstOrDefault();
                if (user != null)
                {
                    Manager.Role = user.Роль;
                    switch(user.Роль)
                    {
                        case 1:
                            Manager.mainFrame.Navigate(new ProductList());
                            break;
                        case 2:
                            Manager.mainFrame.Navigate(new ProductList());
                            break;
                        case 3:
                            Manager.mainFrame.Navigate(new OrderList());
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Введены неверные данные", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Заполнены не все поля", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                txtLogin.Text = "";
                txtPassword.Password = "";
            }
           
        }

        private void btnLoginGuest_Click(object sender, RoutedEventArgs e)
        {
            Manager.Role = 2;
            Manager.mainFrame.Navigate(new ProductList());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if(Order.OrderList.Count > 0)
            {
                Order.OrderList.Clear();
            }
           
        }
    }
}
