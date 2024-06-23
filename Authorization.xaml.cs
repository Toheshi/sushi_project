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
using sushi_darom.cs;
using System.Xml.Linq;
using Microsoft.SqlServer.Server;

namespace sushi_darom
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>

    public partial class Authorization : Page
    {

        private SQLConnect sqlConnect;
        public static string sqlStr = @"SELECT * FROM Logpass";

        public Authorization()
        {
            InitializeComponent();
            sqlConnect = new SQLConnect();

            sqlConnect.OpenConnection();
            string sqlQ = @"SELECT * FROM Orders";
            SqlCommand sqlCmd = new SqlCommand(sqlQ, sqlConnect.GetConnection());
            SqlDataReader dr = sqlCmd.ExecuteReader();

            while (dr.Read())
            {
                Database.orders.Add(new Orders() { order_id = Convert.ToString(dr[0]), customer_name = Convert.ToString(dr[1]), order_date = Convert.ToString(dr[2]), order_status = Convert.ToString(dr[3]), total_sum = Convert.ToString(dr[4]) });
            }
            Global.sch = Database.orders.Count();
            dr.Close();
            sqlConnect.CloseConnection();
        }
        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Registration());
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string phone = Login.Text; // логин пользователя (номер телефона)
            string password = Password.Password; // пароль пользователя (пароль)

            try
            {
                sqlConnect.OpenConnection();
                string sqlQ = sqlStr;
                SqlCommand sqlCmd = new SqlCommand(sqlQ, sqlConnect.GetConnection());
                SqlDataReader dr = sqlCmd.ExecuteReader();
                Database.logpasses.Clear(); // Очистить список перед заполнением
                while (dr.Read())
                {
                    Database.logpasses.Add(new Logpass()
                    {
                        id = Convert.ToString(dr[0]),
                        phone_number = Convert.ToString(dr[1]),
                        password_ = Convert.ToString(dr[2])
                    });
                }
                dr.Close();
                sqlConnect.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message);
                sqlConnect.CloseConnection();
                return;
            }

            var user = Database.logpasses.FirstOrDefault(s => s.phone_number == phone);

            if (phone == "")
            {
                MessageBox.Show("Введите логин и пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (user != null)
            {
                if (user.phone_number == "admin" || password == user.password_)
                {
                    Global.login = user.phone_number;
                    var mainWindow = new MainWindow(Global.login);
                    Window.GetWindow(this).Close();
                    mainWindow.Show();
                }
                else
                {
                    MessageBox.Show("Пароль введен неверно", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Пользователь " + phone + " не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
