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
    /// Логика взаимодействия для AddEditPage1.xaml
    /// </summary>
    public partial class AddEditPage1 : Page
    {
        private Positioninformation _currentPosition = new Positioninformation();
        public AddEditPage1(Positioninformation selectedPosition)
        {
            InitializeComponent();
            if (selectedPosition != null)
            {
                _currentPosition = selectedPosition;
            }
            DataContext = _currentPosition;
        }
        
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult Result = MessageBox.Show("Вы действительно хотите отменить редактирование?", "Предупреждение", MessageBoxButton.YesNo);
            if (Result == MessageBoxResult.Yes)
            {
                Manager1.MainFrame1.Navigate(new Position());
            }
            else if (Result == MessageBoxResult.No)
            {

            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (_currentPosition.PositionCode <= 0)
                errors.AppendLine("Введите Код должности");
            if (string.IsNullOrWhiteSpace(_currentPosition.Name))
                errors.AppendLine("Введите название должности");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentPosition.ID == 0)
                Personnel_DepartmentEntities.GetContext().Positioninformations.Add(_currentPosition);
            try
            {
                Personnel_DepartmentEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!!!");
                Manager.MainFrame.Navigate(new GenPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
