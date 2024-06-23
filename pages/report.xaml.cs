using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace sushi_darom.cs
{
    /// <summary>
    /// Логика взаимодействия для report.xaml
    /// </summary>
    public partial class report : Page
    {
        private SQLConnect sqlConnect;
        double sum = 0;
        public string sqlStr1 = @"SELECT * FROM Orders WHERE order_date";
        string date;
        string dateFrom;
        string dateTo;
        public report()
        {
            InitializeComponent();
            sqlConnect = new SQLConnect();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime? selectedDateFrom = datePicker1.SelectedDate;
            DateTime? selectedDateTo = datePicker2.SelectedDate;

            if (selectedDateFrom.HasValue)
            {
                dateFrom = selectedDateFrom.Value.ToString("yyyy-MM-dd");
            }
            if (selectedDateTo.HasValue)
            {
                dateTo = selectedDateTo.Value.ToString("yyyy-MM-dd");
            }

            try
            {
                sqlConnect.OpenConnection(); // Открытие коннета
                string sqlQ = @"SELECT * FROM Orders WHERE order_date BETWEEN '" + dateFrom + "' AND '" + dateTo + "';"; // Запрос к БД
                SqlCommand sqlCmd = new SqlCommand(sqlQ, sqlConnect.GetConnection());
                SqlDataReader dr = sqlCmd.ExecuteReader();

                Database.orders.Clear();

                while (dr.Read())
                {
                    Database.orders.Add(new Orders() { order_id = Convert.ToString(dr[0]), customer_name = Convert.ToString(dr[1]), order_date = Convert.ToString(dr[2]), order_status = Convert.ToString(dr[3]), comment_ord = Convert.ToString(dr[4]), total_sum = Convert.ToString(dr[5]) });
                }
                dr.Close();
                sqlConnect.CloseConnection(); // Закрытие коннета
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message);
                sqlConnect.CloseConnection();
                return;
            }

            for (int i = 0; i < Database.orders.Count; i++)
            {
                sum += Convert.ToDouble(Database.orders[i].total_sum);
            }

            sumOrder.Text = Database.orders.Count.ToString();
            sumProfit.Text = Convert.ToString(sum) + " руб.";
            sum = 0;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            date = DateTime.Now.ToString("yyyy-MM-dd");

            try
            {
                sqlConnect.OpenConnection(); // Открытие коннета
                string sqlQ = @"SELECT * FROM Orders WHERE order_date ='" + date + "';"; // Запрос к БД
                SqlCommand sqlCmd = new SqlCommand(sqlQ, sqlConnect.GetConnection());
                SqlDataReader dr = sqlCmd.ExecuteReader();

                Database.orders.Clear();

                while (dr.Read())
                {
                    Database.orders.Add(new Orders() { order_id = Convert.ToString(dr[0]), customer_name = Convert.ToString(dr[1]), order_date = Convert.ToString(dr[2]), order_status = Convert.ToString(dr[3]), comment_ord = Convert.ToString(dr[4]), total_sum = Convert.ToString(dr[5]) });
                }
                dr.Close();
                sqlConnect.CloseConnection(); // Закрытие коннета
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message);
                sqlConnect.CloseConnection();
                return;
            }

            for (int i = 0; i < Database.orders.Count; i++)
            {
                sum += Convert.ToDouble(Database.orders[i].total_sum);
            }

            sumOrder.Text = Database.orders.Count.ToString();
            sumProfit.Text = Convert.ToString(sum) + " руб.";
            sum = 0;
        }
    }
}
