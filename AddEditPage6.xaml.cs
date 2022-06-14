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
    /// Логика взаимодействия для AddEditPage6.xaml
    /// </summary>
    public partial class AddEditPage6 : Page
    {
        private Medical_cards _currentMed = new Medical_cards();
        public AddEditPage6(Medical_cards selectedMed)
        {
            InitializeComponent();
            if (selectedMed != null)
            {
                _currentMed = selectedMed;
            }
            DataContext = _currentMed;
            ComboPosition.ItemsSource = Personnel_DepartmentEntities.GetContext().Generalinformations.ToList();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (_currentMed.Generalinformation == null)
                errors.AppendLine("Введите Код сотрудника");
            if (string.IsNullOrWhiteSpace(_currentMed.Name_group))
                errors.AppendLine("Введите название страховой компании");
            if (_currentMed.Insurance_OMC == 0)
                errors.AppendLine("Введите Номер СНИЛСа");
            if (_currentMed.Snils == null)
                errors.AppendLine("Введите Номер СНИЛСа");

            if (errors.Length > 0) 
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentMed.ID == 0)
                Personnel_DepartmentEntities.GetContext().Medical_cards.Add(_currentMed);

            try
            {
                Personnel_DepartmentEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!!!");
                Manager1.MainFrame1.Navigate(new Medical());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult Result = MessageBox.Show("Вы действительно хотите отменить редактирование?", "Предупреждение", MessageBoxButton.YesNo);
            if (Result == MessageBoxResult.Yes)
            {
                Manager.MainFrame.Navigate(new Medical());
            }
            else if (Result == MessageBoxResult.No)
            {

            }
        }
    }
}
