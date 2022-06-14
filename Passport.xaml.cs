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
    /// Логика взаимодействия для Passport.xaml
    /// </summary>
    public partial class Passport : Page
    {
        public Passport()
        {
            InitializeComponent();
            DGridPassport.ItemsSource = Personnel_DepartmentEntities.GetContext().PassportDocuments.ToList();
            Update();
        }
        private void Update()
        {
            var currentpas = Personnel_DepartmentEntities.GetContext().PassportDocuments.ToList();

            currentpas = currentpas.Where(p => p.Generalinformation.FIO.ToString().ToLower().Contains(Search_Passport.Text.ToLower())).ToList();


            // привязка данных для отображения в DataGrid данных из таблицы БД
            DGridPassport.ItemsSource = currentpas;
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var PositionForRemoving = DGridPassport.SelectedItems.Cast<PassportDocument>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {PositionForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Personnel_DepartmentEntities.GetContext().PassportDocuments.RemoveRange(PositionForRemoving);
                    Personnel_DepartmentEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    DGridPassport.ItemsSource = Personnel_DepartmentEntities.GetContext().PassportDocuments.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void AddBtn3_Click(object sender, RoutedEventArgs e)
        {
            Manager1.MainFrame1.Navigate(new AddEditPage3(null));
        }

        private void BtnEdit3_Click(object sender, RoutedEventArgs e)
        {
            Manager1.MainFrame1.Navigate(new AddEditPage3((sender as Button).DataContext as PassportDocument));
        }

        private void Exp_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Visible = true;
            Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Microsoft.Office.Interop.Excel.Worksheet sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];

            for (int j = 0; j < DGridPassport.Columns.Count; j++)
            {
                Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[1, j + 1];
                sheet1.Cells[1, j + 1].Font.Bold = true;
                sheet1.Columns[j + 1].ColumnWidth = 15;
                myRange.Value2 = DGridPassport.Columns[j].Header;
            }
            for (int i = 0; i < DGridPassport.Columns.Count; i++)
            {
                for (int j = 0; j < DGridPassport.Items.Count; j++)
                {
                    TextBlock b = DGridPassport.Columns[i].GetCellContent(DGridPassport.Items[j]) as TextBlock;
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                    if (b != null)
                    {
                        myRange.Value2 = b.Text;
                    }
                    else
                    {
                        myRange.Value2 = "";
                    }
                }
                sheet1.Columns.EntireColumn.AutoFit();
            }
        }

        private void Search_Passport_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }
    }
}
