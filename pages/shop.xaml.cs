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
using System.Xml.Linq;

namespace sushi_darom.cs
{
    /// <summary>
    /// Логика взаимодействия для shop.xaml
    /// </summary>


    public partial class shop : Page
    {
        private SQLConnect sqlConnect;

        public int sch = 1;

        string sqlStr1;
        public string sumOrd;
        public string customerName;
        public string addressCust;
        public string commentOrd;
        public string prodName;
        public string prodPrice;

        double sum = 0; // сумма за заказ

        public shop()
        {
            InitializeComponent();

         
            for (int i = 0; i < Database.cart.Count; i++)
            {
                sum += Convert.ToDouble(Database.cart[i].product_price);
            }

            sumOrder.Text = sum.ToString();
            sumOrd = sumOrder.Text;
            cartList.ItemsSource = Database.cart.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var button = sender as Button;
            var dataContext = button.DataContext;

            if (dataContext is Cart item)
            {
                sum -= Convert.ToDouble(item.product_price);
                Database.cart.Remove(item);
            }
            sumOrder.Text = sum.ToString();
            sumOrd = sumOrder.Text;
            cartList.ItemsSource = Database.cart.ToList();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            sqlConnect = new SQLConnect();

            if (Database.cart.Count == 0)
            {
                MessageBox.Show("Товары в корзине на найдены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                customerName = customName.Text;
                addressCust = addrCust.Text;
                commentOrd = commOrd.Text;

                if (customerName == "")
                {
                    MessageBox.Show("Укажите ФИО", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if (addressCust == "")
                {
                    MessageBox.Show("Укажите адрес", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    sqlStr1 = @"INSERT INTO Orders (customer_name, order_date, order_status, total_sum, comment_ord) VALUES ('" + customerName + "', CONVERT(date, GETDATE()), 'Новый',"
                        + sumOrd + ",'" + commentOrd + "');";

                    try
                    {
                        sqlConnect.OpenConnection(); // Открытие коннета
                        string sqlQ = sqlStr1; // Запрос к БД
                        SqlCommand sqlCmd = new SqlCommand(sqlQ, sqlConnect.GetConnection());
                        SqlDataReader dr = sqlCmd.ExecuteReader();
                        sqlConnect.CloseConnection(); // Закрытие коннета
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Произошла ошибка: " + ex.Message);
                        sqlConnect.CloseConnection();
                        return;
                    }
                    Global.sch++;
                    for (int i = 0; i < Database.cart.Count; i++)
                    {
                        string prodPrice = Database.cart[i].product_price;
                        prodPrice = prodPrice.Replace(',', '.');
                        string sch_ord = Convert.ToString(Global.sch);

                        sqlConnect.OpenConnection();
                        string sqlQ = @"INSERT INTO OrdersDetails (order_id, product_name, product_price, order_date, address_) VALUES 
                        (" + sch_ord + ",'" + Database.cart[i].product_name + "'," + prodPrice + ", GETDATE(), '" + addressCust + "');"; // Запрос к БД
                        SqlCommand sqlCmd = new SqlCommand(sqlQ, sqlConnect.GetConnection());
                        SqlDataReader dr = sqlCmd.ExecuteReader();
                        sqlConnect.CloseConnection();
                    }

                    Database.cart.Clear();
                    MessageBox.Show("Заказ оформлен! Оплатите заказ", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}
