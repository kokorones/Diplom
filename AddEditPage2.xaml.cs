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
    /// Логика взаимодействия для AddEditPage2.xaml
    /// </summary>
    public partial class AddEditPage2 : Page
    {
        private Generalinformation _currentGeneral = new Generalinformation();
        public AddEditPage2(Generalinformation selectedGeneral)
        {
            InitializeComponent();
            if (selectedGeneral != null)
            {
                _currentGeneral = selectedGeneral;
            }
            DataContext = _currentGeneral;
            ComboPosition.ItemsSource = Personnel_DepartmentEntities.GetContext().Positioninformations.ToList();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (_currentGeneral.Personnel_Number <= 0)
                errors.AppendLine("Введите Код должности");
            if (_currentGeneral.Personnel_Number != int.MaxValue && _currentGeneral.Personnel_Number
                != int.MinValue && _currentGeneral.Personnel_Number < 0)
                errors.AppendLine("Введите правильный код сотрудника");
            if (string.IsNullOrWhiteSpace(_currentGeneral.FIO))
                errors.AppendLine("Введите Ф.И.О");
            if (string.IsNullOrWhiteSpace(_currentGeneral.Gender))
                errors.AppendLine("Введите Пол");
            if (_currentGeneral.Positioninformation == null)
                errors.AppendLine("Введите Должность");
            if (string.IsNullOrWhiteSpace(_currentGeneral.Phone))
                errors.AppendLine("Введите Телефон");
            if (string.IsNullOrWhiteSpace(_currentGeneral.Adress))
                errors.AppendLine("Введите Адрес");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentGeneral.ID == 0)
                Personnel_DepartmentEntities.GetContext().Generalinformations.Add(_currentGeneral);

            try
            {
                Personnel_DepartmentEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!!!");
                Manager1.MainFrame1.Navigate(new Generalinfo());
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
                Manager1.MainFrame1.Navigate(new Generalinfo());
            }
            else if (Result == MessageBoxResult.No)
            {

            }
        }
    }
}
