using sushi_darom.cs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using System.Security.Policy;

namespace sushi_darom
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(string login)
        {
            InitializeComponent();

            if (login != null)
            {
                if (login == "admin")
                {
                    admin_btn.Content = "ADMIN";
                    admin_btn1.Visibility = Visibility.Visible;
                    admin_btn2.Visibility = Visibility.Visible;
                    admin_btn3.Visibility = Visibility.Visible;
                }
                else
                {
                    admin_btn.Content = "USER";
                    admin_btn.Background = new SolidColorBrush(Colors.Orange);
                    admin_btn1.Visibility = Visibility.Hidden;
                    admin_btn2.Visibility = Visibility.Hidden;
                    admin_btn3.Visibility = Visibility.Hidden;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) // Товары
        {          
            products products = new products();
            mainframe.NavigationService.Navigate(products);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // Контакты
        {
            contacts contacts = new contacts();
            mainframe.NavigationService.Navigate(contacts);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) // Добавление/удаление продуктов
        {
            stngProd stngProd = new stngProd();
            mainframe.NavigationService.Navigate(stngProd);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e) // Заказы
        {
            orders orders = new orders();
            mainframe.NavigationService.Navigate(orders);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e) // Отчеты
        {
            report report = new report();
            mainframe.NavigationService.Navigate(report);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e) // Корзина
        {
            shop shop = new shop();
            mainframe.NavigationService.Navigate(shop);
        }

        private void Button_Click_6(object sender, RoutedEventArgs e) // Сеты
        {
            products.sqlStr = "SELECT id_products, name_pr, price_pr, description_pr, picture FROM Products JOIN Category ON Products.id_category = Category.id WHERE Category.name_category = 'Сеты'";
            products Products = new products();

            mainframe.NavigationService.Navigate(Products);
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            products.sqlStr = "SELECT id_products, name_pr, price_pr, description_pr, picture FROM Products JOIN Category ON Products.id_category = Category.id WHERE Category.name_category = 'Роллы'";
            products Products = new products();

            mainframe.NavigationService.Navigate(Products);
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            products.sqlStr = "SELECT id_products, name_pr, price_pr, description_pr, picture FROM Products JOIN Category ON Products.id_category = Category.id WHERE Category.name_category = 'Суши'";
            products Products = new products();

            mainframe.NavigationService.Navigate(Products);
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            products.sqlStr = "SELECT id_products, name_pr, price_pr, description_pr, picture FROM Products JOIN Category ON Products.id_category = Category.id WHERE Category.name_category = 'Напитки'";
            products Products = new products();

            mainframe.NavigationService.Navigate(Products);
        }

        private void admin_btn_Click(object sender, RoutedEventArgs e)
        {
            AuthReg auth = new AuthReg();
            auth.Show();
            Window.GetWindow(this).Close();
        }
    }
}
