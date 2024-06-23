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

namespace sushi_darom.cs
{
    /// <summary>
    /// Логика взаимодействия для products.xaml
    /// </summary>
    public partial class products : Page
    {
        private SQLConnect sqlConnect;
        public static string sqlStr = @"SELECT * FROM Products";
        public List<string> cart1 = new List<string>();
        public products()
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
                }
                dr.Close();
                sqlConnect.CloseConnection(); // Закрытие коннета

                prodList.ItemsSource = Database.products.ToList();
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
            if (dataContext is Products item)
            {
                Database.cart.Add(new Cart() { product_name = item.name_pr, product_price = item.price_pr });
            }
        }
    }
}
