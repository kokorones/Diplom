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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для EnterPage.xaml
    /// </summary>
    public partial class EnterPage : Page
    {
        public EnterPage()
        {
            InitializeComponent();
        }

        public static class Global
        {
            public static string role = "";
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            if( string.IsNullOrEmpty(TextBox.Text) || string.IsNullOrEmpty(PasswordBox.Password))
            {
                MessageBox.Show("Введите логин и пароль");
            }
            using (var db = new Personnel_DepartmentEntities())
            {
                var user = db.Users
                    .AsNoTracking()
                    .FirstOrDefault(u => u.Login == TextBox.Text && u.Password == PasswordBox.Password);

                if(user == null)
                {
                    MessageBox.Show("Пользователь не найден!");
                    return;
                }
                switch (user.Role)
                {
                    case "Сотрудник":
                        Global.role = "Сотрудник";
                        Manager.MainFrame.Navigate(new GenPage());
                        break;

                    case "Администратор":
                        Global.role = "Администратор";
                        Manager.MainFrame.Navigate(new GenPage());
                        break;


                }
            }
        }
    }
}
