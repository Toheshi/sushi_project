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
using sushi_darom.pages;
using System.Diagnostics;
using System.Xml.Linq;
using Microsoft.Win32;

namespace sushi_darom.cs
{
    /// <summary>
    /// Логика взаимодействия для stngProd.xaml
    /// </summary>

    
    public partial class stngProd : Page
    {
        private SQLConnect sqlConnect;
        private string imagePath;
        static string id = "";
        public static string sqlStr = @"SELECT * FROM Category";
        //public static string sqlStr1 = 

        static string name_prod;
        static string price_prod;
        static string descr_prod;

        public List<string> category_old = new List<string>();
        public List<string> category_new = new List<string>();
        

        public stngProd()
        {
            InitializeComponent();
            sqlConnect = new SQLConnect();

            try
            {
                sqlConnect.OpenConnection();
                string sqlQ = sqlStr;
                SqlCommand sqlCmd = new SqlCommand(sqlQ, sqlConnect.GetConnection());
                SqlDataReader dr = sqlCmd.ExecuteReader();
                Database.products.Clear();


                while (dr.Read())
                {
                    category_old.Add(Convert.ToString(dr[0])); // id
                    category_old.Add(Convert.ToString(dr[1])); // name
                }

                dr.Close();
                sqlConnect.CloseConnection();

                for (int i = 0; i < category_old.Count; i++)
                {
                    if (i % 2 == 1)
                    {
                        category_new.Add(category_old[i]); // name
                    }
                }

                categoryBox.ItemsSource = category_new;

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
            string categor = categoryBox.Text;
            
            name_prod = nameProd.Text;
            price_prod = priceProd.Text;
            descr_prod = descrProd.Text;

            for (int i = 0; i < category_old.Count; i++)
            {
                if (categor == category_old[i])
                {
                    id = category_old[i - 1];
                }
            }

            try
            {
                sqlConnect.OpenConnection();
                string sqlQ = @"INSERT INTO Products(name_pr, price_pr, description_pr, picture, id_category)
VALUES('" + name_prod + "','" + price_prod + "','" + descr_prod + "','" + imagePath + "','" + id + "');"; ;
                SqlCommand sqlCmd = new SqlCommand(sqlQ, sqlConnect.GetConnection());
                SqlDataReader dr = sqlCmd.ExecuteReader();
                while (dr.Read())
                {
                    Database.products.Add(new Products() { name_pr = Convert.ToString(dr[0]), price_pr = Convert.ToString(dr[1]), description_pr = Convert.ToString(dr[2]), picture = Convert.ToString(dr[3]), id_category = Convert.ToString(dr[4]) });
                }
                sqlConnect.CloseConnection();

                MessageBox.Show("Товар успешно добавлен!", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message);
                sqlConnect.CloseConnection();
                return;
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DeleteProd deleteProd = new DeleteProd();
            deleteProd.Show();
        }

        private void SelectImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                // Сохранение пути к выбранному файлу в переменную
                imagePath = openFileDialog.FileName;

                //// Загрузка и отображение изображения
                BitmapImage bitmap = new BitmapImage(new Uri(imagePath));
                SelectedImage.Source = bitmap;
            }
        }
    }
    
}
