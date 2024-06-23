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
using System.Windows.Controls.Primitives;
using System.Security.Cryptography.X509Certificates;

namespace sushi_darom.cs
{
    /// <summary>
    /// Логика взаимодействия для orders.xaml
    /// </summary>
    public partial class orders : Page
    {
        private SQLConnect sqlConnect;

        public string sqlStr1 = @"SELECT * FROM Orders";
        public List<string> delList = new List<string>();
        public static string delProd;
        public orders()
        {
            InitializeComponent();
            ordersUpdate();
        }
        public void ordersUpdate()
        {
            sqlConnect = new SQLConnect();
            try
            {
                sqlConnect.OpenConnection(); // Открытие коннета
                string sqlQ = sqlStr1; // Запрос к БД
                SqlCommand sqlCmd = new SqlCommand(sqlQ, sqlConnect.GetConnection());
                SqlDataReader dr = sqlCmd.ExecuteReader();

                Database.orders.Clear();

                while (dr.Read())
                {
                    Database.orders.Add(new Orders() { order_id = Convert.ToString(dr[0]), customer_name = Convert.ToString(dr[1]), order_date = Convert.ToString(dr[2]), order_status = Convert.ToString(dr[3]), comment_ord = Convert.ToString(dr[4]), total_sum = Convert.ToString(dr[5]) });
                }
                dr.Close();
                sqlConnect.CloseConnection(); // Закрытие коннета

                orderList.ItemsSource = Database.orders.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message);
                sqlConnect.CloseConnection();
                return;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var dataContext = button.DataContext;

            if (dataContext is Orders item)
            {
                try
                {
                    sqlConnect.OpenConnection(); // Открытие коннета
                    string sqlQ = @"SELECT * FROM OrdersDetails WHERE order_id =" + item.order_id + ";"; // Запрос к БД
                    SqlCommand sqlCmd = new SqlCommand(sqlQ, sqlConnect.GetConnection());
                    SqlDataReader dr = sqlCmd.ExecuteReader();

                    Database.orderDetails.Clear();

                    while (dr.Read())
                    {
                        Database.orderDetails.Add(new OrderDetails() { order_id = Convert.ToString(dr[0]), product_name = Convert.ToString(dr[1]), product_price = Convert.ToString(dr[2]), order_date = Convert.ToString(dr[3]), address_ = Convert.ToString(dr[4])});
                    }
                    dr.Close();
                    sqlConnect.CloseConnection(); // Закрытие коннета

                    orderDetList.ItemsSource = Database.orderDetails.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка: " + ex.Message);
                    sqlConnect.CloseConnection();
                    return;
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var dataContext = button.DataContext;

            if (dataContext is Orders item)
            {
                try
                {
                    sqlConnect.OpenConnection(); // Открытие коннета
                    string sqlQ = @"UPDATE Orders SET order_status = 'Готов' WHERE customer_name = +'" + item.customer_name + "' AND order_date = CAST(GETDATE() AS date);"; // Запрос к БД
                    SqlCommand sqlCmd = new SqlCommand(sqlQ, sqlConnect.GetConnection());
                    SqlDataReader dr = sqlCmd.ExecuteReader();

                    Database.orders.Clear();

                    while (dr.Read())
                    {
                        Database.orders.Add(new Orders() { order_id = Convert.ToString(dr[0]), customer_name = Convert.ToString(dr[1]), order_date = Convert.ToString(dr[2]), order_status = Convert.ToString(dr[3]), comment_ord = Convert.ToString(dr[4]), total_sum = Convert.ToString(dr[5]) });
                    }
                    dr.Close();
                    sqlConnect.CloseConnection(); // Закрытие коннета 

                    orderList.ItemsSource = Database.orders.ToList();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка: " + ex.Message);
                    sqlConnect.CloseConnection();
                    return;
                }
            }
            ordersUpdate();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var dataContext = button.DataContext;

            if (dataContext is Orders item)
            {
                Database.orders.Remove(new Orders() { order_id = item.order_id });
                delProd = item.order_id;
            }

            try
            {
                sqlConnect.OpenConnection(); // Открытие коннета
                string sqlQ = @"DELETE FROM Orders WHERE order_id = '" + delProd + "'";// Запрос к БД
                SqlCommand sqlCmd = new SqlCommand(sqlQ, sqlConnect.GetConnection());
                SqlDataReader dr = sqlCmd.ExecuteReader();
                Database.products.Clear();
                orderDetList.ItemsSource = Database.products.ToList();
                Window.GetWindow(this).Close();
                MessageBox.Show("Заказ успешно удален!", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message);
                sqlConnect.CloseConnection();
                return;
            }
        }
    }
}
