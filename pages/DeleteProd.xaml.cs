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
using System.Windows.Shapes;

namespace sushi_darom.pages
{
    /// <summary>
    /// Логика взаимодействия для DeleteProd.xaml
    /// </summary>
    public partial class DeleteProd : Window
    {
        private SQLConnect sqlConnect;
        public static string sqlStr = @"SELECT * FROM Products";
        public List<string> delList = new List<string>();
        public static string delProd;

        public DeleteProd()
        {
            InitializeComponent();
            sqlConnect = new SQLConnect();

            try
            {
                sqlConnect.OpenConnection(); // Открытие коннета
                string sqlQ = sqlStr; // Запрос к БД
                SqlCommand sqlCmd = new SqlCommand(sqlQ, sqlConnect.GetConnection());
                SqlDataReader dr = sqlCmd.ExecuteReader();
                Database.products.Clear();

                while (dr.Read())
                {
                    Database.products.Add(new Products() { id_products = Convert.ToString(dr[0]), name_pr = Convert.ToString(dr[1]), price_pr = Convert.ToString(dr[2]), description_pr = Convert.ToString(dr[3]), picture = Convert.ToString(dr[4]) });
                    delList.Add(Convert.ToString(dr[1]));
                }
                dr.Close();
                sqlConnect.CloseConnection(); // Закрытие коннета

                prodList1.ItemsSource = Database.products.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message);
                sqlConnect.CloseConnection();
                return;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) // "Удалить"
        {
            var button = sender as Button;
            var dataContext = button.DataContext;

            if (dataContext is Products item)
            {
                Database.products.Remove(new Products() { name_pr = item.name_pr });
                delProd = item.name_pr;
            }

            try
            {
                sqlConnect.OpenConnection(); // Открытие коннета
                string sqlQ = @"DELETE FROM Products WHERE name_pr = '" + delProd + "'";// Запрос к БД
                SqlCommand sqlCmd = new SqlCommand(sqlQ, sqlConnect.GetConnection());
                SqlDataReader dr = sqlCmd.ExecuteReader();
                Database.products.Clear();
                prodList1.ItemsSource = Database.products.ToList();
                Window.GetWindow(this).Close();
                MessageBox.Show("Товар успешно удален!", "", MessageBoxButton.OK, MessageBoxImage.Information);
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
