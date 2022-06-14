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
    /// Логика взаимодействия для AddEditPage4.xaml
    /// </summary>
    public partial class AddEditPage4 : Page
    {
        private EducationDocument _currentEdu = new EducationDocument();  
        public AddEditPage4(EducationDocument selectedEdu)
        {
            InitializeComponent();
            if (selectedEdu != null)
            {
                _currentEdu = selectedEdu;
            }
            DataContext = _currentEdu;
            ComboPosition.ItemsSource = Personnel_DepartmentEntities.GetContext().Generalinformations.ToList();
        }

        private void BtnSave2_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (_currentEdu.Generalinformation == null)
                errors.AppendLine("Введите Код сотрудника");
            if (_currentEdu.Document_number == 0)
                errors.AppendLine("Введите № диплома");
            if (string.IsNullOrWhiteSpace(_currentEdu.Education))
                errors.AppendLine("Введите Образование");
            if (string.IsNullOrWhiteSpace(_currentEdu.Specialty))
                errors.AppendLine("Введите Специальность");
            if (string.IsNullOrWhiteSpace(_currentEdu.EducationlInstitution))
                errors.AppendLine("Введите Учреждение");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentEdu.ID == 0)
                Personnel_DepartmentEntities.GetContext().EducationDocuments.Add(_currentEdu);

            try
            {
                Personnel_DepartmentEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!!!");
                Manager1.MainFrame1.Navigate(new Education());
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
                Manager.MainFrame.Navigate(new Education());
            }
            else if (Result == MessageBoxResult.No)
            {

            }
        }
    }
}
