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
    /// Логика взаимодействия для GenPage.xaml
    /// </summary>
    public partial class GenPage : Page
    {
        public GenPage()
        {
            InitializeComponent();
            Manager1.MainFrame1 = MainFrame1;
            MainFrame1.Navigate(new Position());
            if (EnterPage.Global.role == "Сотрудник")
            {
                Position_Btn.IsEnabled = false;
                Medical_btn.IsEnabled = false;
                MilBtn.IsEnabled = false;
                Manager1.MainFrame1.Navigate(new Generalinfo());
            }
        }

        private void Position_Btn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame1.Navigate(new Position()); //AddEditPage1
        }

        private void Passport_Btn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame1.Navigate(new Passport()); //AddEditPage3
        }

        private void Education_Btn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame1.Navigate(new Education()); //AddEditPage4
        }

        private void Info_btn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame1.Navigate(new Generalinfo()); //AddEditPage2
        }

        

        private void Medical_btn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame1.Navigate(new Medical());
        }

        private void Exit_btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult Result = MessageBox.Show("Вы действительно хотите закрыть приложение?", "Предупреждение", MessageBoxButton.YesNo);
            if (Result == MessageBoxResult.Yes)
            {
                Application.Current.MainWindow.Close();
            }
            else if (Result == MessageBoxResult.No)
            {

            }
        }

        private void MilBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame1.Navigate(new Ticket()); //AddEditPage5
        }

        private void ButtonDocument_Click(object sender, RoutedEventArgs e)
        {
            MainFrame1.Navigate(new Document()); //AddEditPage2
        }
    }
}
