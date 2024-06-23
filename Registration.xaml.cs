using System;
using System.Collections;
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

namespace sushi_darom
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        private SQLConnect sqlConnect;
        private static string pnum;
        private static string password;
        private static string flastname;

        private static string sqlStr1 = @"INSERT INTO Logpass (phone_number, password_) VALUES('" + pnum + "'," + "'" + password + "');";
        private static string sqlStr2 = @"INSERT INTO Customers (name_cm, phone_number) VALUES('" + flastname + "','" + pnum + "');";
        public Registration()
        {
            InitializeComponent();
            sqlConnect = new SQLConnect();
        }

        private void TextBlock_MouseLeftButtonDown2(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Authorization());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            pnum = pn.Text;
            password = pswrd.Password;
            flastname = fio.Text;

            string sqlStr1 = @"INSERT INTO Logpass (phone_number, password_) VALUES(@phone_number, @password_);";
            string sqlStr2 = @"INSERT INTO Customers (name_cm, phone_number) VALUES(@name_cm, @phone_number);";

            if (pnum == "" && password == "" && flastname == "")
            {
                MessageBox.Show("Заполните данные!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                try
                {
                    sqlConnect.OpenConnection(); // Открытие соединения

                    // Проверка существования phone_number в Logpass
                    string sqlCheck = "SELECT COUNT(*) FROM Logpass WHERE phone_number = @phone_number";
                    SqlCommand checkLogin = new SqlCommand(sqlCheck, sqlConnect.GetConnection());
                    checkLogin.Parameters.AddWithValue("@phone_number", pnum);
                    int count = Convert.ToInt32(checkLogin.ExecuteScalar());

                    if (count == 0)
                    {
                        SqlCommand sqlCmd1 = new SqlCommand(sqlStr1, sqlConnect.GetConnection());
                        sqlCmd1.Parameters.AddWithValue("@phone_number", pnum);
                        sqlCmd1.Parameters.AddWithValue("@password_", password);
                        sqlCmd1.ExecuteNonQuery();
                    }
                    sqlConnect.CloseConnection();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка: " + ex.Message);
                    sqlConnect.CloseConnection();
                    return;
                }

                try
                {
                    sqlConnect.OpenConnection(); // Открытие соединения

                    // Проверка существования phone_number в Customers
                    string sqlCheck = "SELECT COUNT(*) FROM Customers WHERE phone_number = @phone_number";
                    SqlCommand checkLogin = new SqlCommand(sqlCheck, sqlConnect.GetConnection());
                    checkLogin.Parameters.AddWithValue("@phone_number", pnum);
                    int customerCount = Convert.ToInt32(checkLogin.ExecuteScalar());

                    if (customerCount == 0)
                    {
                        SqlCommand sqlCmd2 = new SqlCommand(sqlStr2, sqlConnect.GetConnection());
                        sqlCmd2.Parameters.AddWithValue("@name_cm", flastname);
                        sqlCmd2.Parameters.AddWithValue("@phone_number", pnum);
                        sqlCmd2.ExecuteNonQuery();

                        MessageBox.Show("Вы зарегистрированы!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Этот номер телефона/логин уже существует!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    sqlConnect.CloseConnection();

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
}
