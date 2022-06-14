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
    /// Логика взаимодействия для AddEditPage3.xaml
    /// </summary>
    public partial class AddEditPage3 : Page
    {
        private PassportDocument _currentPassport = new PassportDocument();
        public AddEditPage3(PassportDocument selectedPassport)
        { 

            InitializeComponent();
            if (selectedPassport != null)
            {
                _currentPassport = selectedPassport;
            }
            DataContext = _currentPassport;
            ComboPosition.ItemsSource = Personnel_DepartmentEntities.GetContext().Generalinformations.ToList();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (_currentPassport.Generalinformation == null)
                errors.AppendLine("Введите Код сотрудника");
            if (_currentPassport.NumberSeries == null || _currentPassport.NumberSeries.Length != 11)
                errors.AppendLine("Введите Номер паспорта (Пропишите номер и серию паспорта)");
            if (string.IsNullOrWhiteSpace(_currentPassport.Citizenship))
                errors.AppendLine("Введите Гражданство");
            if (string.IsNullOrWhiteSpace(_currentPassport.IssuedBy))
                errors.AppendLine("Введите Кем выдан паспорт");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentPassport.ID == 0)
                Personnel_DepartmentEntities.GetContext().PassportDocuments.Add(_currentPassport);

            try
            {
                Personnel_DepartmentEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!!!");
                Manager1.MainFrame1.Navigate(new Passport());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult Result = MessageBox.Show("Вы действительно хотите отменить редактирование?", "Предупреждение", MessageBoxButton.YesNo);
            if (Result == MessageBoxResult.Yes)
            {
                Manager.MainFrame.Navigate(new Passport());
            }
            else if (Result == MessageBoxResult.No)
            {

            }
        }
    }
}
