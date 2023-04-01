using Jewelery.Classes;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
    /// Логика взаимодействия для ProductAddEdit.xaml
    /// </summary>
    public partial class ProductAddEdit : Page
    {
        private bool isRedact;
        private string article;
        public ProductAddEdit()
        {
            InitializeComponent();
            isRedact = false;
            Manager.Title.Content = "Добавление товара";
            cmbUnit.ItemsSource = ContextManager.GetContext().Единицы_измерения.ToList();
            cmbUnit.SelectedIndex = 0;
            cmbCategory.ItemsSource = ContextManager.GetContext().Категории.ToList();
            cmbCategory.SelectedIndex = 0;
            cmbManufacture.ItemsSource = ContextManager.GetContext().Производители.ToList();
            cmbManufacture.SelectedIndex = 0;
            cmbPostavshik.ItemsSource = ContextManager.GetContext().Поставщики.ToList();
            cmbPostavshik.SelectedIndex = 0;

        }

        public ProductAddEdit(VW_ProductList product)
        {
            InitializeComponent();
            isRedact = true;
            Manager.Title.Content = "Редактирование товара";
            txtArticle.Text = product.Артикул;
            article = txtArticle.Text;
            txtName.Text = product.Наименование;
            txtCount.Text = product.Кол_во_на_складе.ToString();
            txtDescription.Text = product.Описание;
            txtMaxSale.Text = product.Размер_максимально_возможной_скидки.ToString();
            txtPrice.Text = product.Стоимость.ToString();
            txtSale.Text = product.Действующая_скидка.ToString();
            
            
            cmbUnit.ItemsSource = ContextManager.GetContext().Единицы_измерения.ToList();
            cmbUnit.SelectedIndex = ContextManager.GetContext().Единицы_измерения.Where(x => x.Код == product.Единица_измерения).FirstOrDefault().Код - 1;
            cmbCategory.ItemsSource = ContextManager.GetContext().Категории.ToList();
            cmbCategory.SelectedIndex = ContextManager.GetContext().Категории.Where(x => x.Код == product.Категория_товара).FirstOrDefault().Код - 1;
            cmbManufacture.ItemsSource = ContextManager.GetContext().Производители.ToList();
            cmbManufacture.SelectedIndex = ContextManager.GetContext().Производители.Where(x => x.Код == product.Производитель).FirstOrDefault().Код - 1;
            cmbPostavshik.ItemsSource = ContextManager.GetContext().Поставщики.ToList();
            cmbPostavshik.SelectedIndex = ContextManager.GetContext().Поставщики.Where(x => x.Код == product.Поставщик).FirstOrDefault().Код - 1;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (isRedact)
            {
                var product = ContextManager.GetContext().Продукция.Where(x => x.Артикул == article).FirstOrDefault();
                product.Артикул = txtArticle.Text.ToString();
                product.Наименование = txtName.Text.ToString();
                product.Единица_измерения = (cmbUnit.SelectedItem as Единицы_измерения).Код;
                product.Стоимость = decimal.Parse(txtPrice.Text.ToString());
                product.Размер_максимально_возможной_скидки = byte.Parse(txtMaxSale.Text.ToString());
                product.Производитель = (cmbManufacture.SelectedItem as Производители).Код;
                product.Поставщик = (cmbPostavshik.SelectedItem as Поставщики).Код;
                product.Категория_товара = (cmbCategory.SelectedItem as Категории).Код;
                product.Действующая_скидка = byte.Parse(txtSale.Text.ToString());
                product.Кол_во_на_складе = byte.Parse(txtCount.Text.ToString());
                product.Описание = txtDescription.Text.ToString();
                ContextManager.GetContext().SaveChanges();
                MessageBox.Show("Товар отредактирован", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                ContextManager.GetContext().Продукция.Add(new Продукция
                {
                    Артикул = txtArticle.Text.ToString(),
                    Наименование = txtName.Text.ToString(),
                    Единица_измерения = (cmbUnit.SelectedItem as Единицы_измерения).Код,
                    Стоимость = int.Parse(txtPrice.Text.ToString()),
                    Размер_максимально_возможной_скидки = byte.Parse(txtMaxSale.Text.ToString()),
                    Производитель = (cmbManufacture.SelectedItem as Производители).Код,
                    Поставщик = (cmbPostavshik.SelectedItem as Поставщики).Код,
                    Категория_товара = (cmbCategory.SelectedItem as Категории).Код,
                    Действующая_скидка = byte.Parse(txtSale.Text.ToString()),
                    Кол_во_на_складе = byte.Parse(txtCount.Text.ToString()),
                    Описание = txtDescription.Text.ToString()
                });
                ContextManager.GetContext().SaveChanges();
                MessageBox.Show("Товар добавлен", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            Manager.mainFrame.GoBack();
        }
    }
}
