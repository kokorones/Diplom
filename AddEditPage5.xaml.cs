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
    /// Логика взаимодействия для AddEditPage5.xaml
    /// </summary>
    public partial class AddEditPage5 : Page
    {
        private Military _currentMilitary = new Military();
        public AddEditPage5(Military selectedMil)
        {
            InitializeComponent();
            if (selectedMil != null)
            {
                _currentMilitary = selectedMil;
            }
            DataContext = _currentMilitary;
            ComboPosition.ItemsSource = Personnel_DepartmentEntities.GetContext().Generalinformations.ToList();
        }

        private void BtnSave_Click_1(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (_currentMilitary.Generalinformation == null)
                errors.AppendLine("Введите Код сотрудника");
            if (_currentMilitary.Series == null)
                errors.AppendLine("Введите Номер паспорта (Серия и паспорт состоят из 11 цифр)");
            if (_currentMilitary.Number == null)
                errors.AppendLine("Введите Номер паспорта (Серия и паспорт состоят из 11 цифр)");
            if (string.IsNullOrWhiteSpace(_currentMilitary.IssuedBy))
                errors.AppendLine("Введите Кем выдан паспорт");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentMilitary.ID == 0)
                Personnel_DepartmentEntities.GetContext().Militaries.Add(_currentMilitary);

            try
                {
                    Personnel_DepartmentEntities.GetContext().SaveChanges();
                    MessageBox.Show("Информация сохранена!!!");
                    Manager1.MainFrame1.Navigate(new Ticket());
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
                Manager.MainFrame.Navigate(new Ticket());
            }
            else if (Result == MessageBoxResult.No)
            {

            }
        }
    }
}
