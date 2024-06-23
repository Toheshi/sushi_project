using System;
using System.Collections.Generic;
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

namespace sushi_darom
{
    /// <summary>
    /// Логика взаимодействия для AuthReg.xaml
    /// </summary>

    public static class Global
    {
        public static int sch;
        public static string login;
    }
    public partial class AuthReg : Window
    {
        public AuthReg()
        {
            InitializeComponent();
            Authorization auth = new Authorization();
            start_frame.NavigationService.Navigate(auth);
        }
    }
}
