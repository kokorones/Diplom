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
    /// Логика взаимодействия для Education.xaml
    /// </summary>
    public partial class Education : Page
    {
        public Education()
        {
            InitializeComponent();
            DGridEducation.ItemsSource = Personnel_DepartmentEntities.GetContext().EducationDocuments.ToList();
            Update();
        }
        private void Update()
        {
            var currentedu = Personnel_DepartmentEntities.GetContext().EducationDocuments.ToList();

            currentedu = currentedu.Where(p => p.Generalinformation.FIO.ToString().ToLower().Contains(Search_Education.Text.ToLower())).ToList();


            // привязка данных для отображения в DataGrid данных из таблицы БД
            DGridEducation.ItemsSource = currentedu;
        }

        private void BtnEdit2_Click(object sender, RoutedEventArgs e)
        {
            Manager1.MainFrame1.Navigate(new AddEditPage4((sender as Button).DataContext as EducationDocument));
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager1.MainFrame1.Navigate(new AddEditPage4(null));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var PositionForRemoving = DGridEducation.SelectedItems.Cast<EducationDocument>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {PositionForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Personnel_DepartmentEntities.GetContext().EducationDocuments.RemoveRange(PositionForRemoving);
                    Personnel_DepartmentEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    DGridEducation.ItemsSource = Personnel_DepartmentEntities.GetContext().EducationDocuments.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void BtnExp_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Visible = true;
            Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Microsoft.Office.Interop.Excel.Worksheet sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];

            for (int j = 0; j < DGridEducation.Columns.Count; j++)
            {
                Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[1, j + 1];
                sheet1.Cells[1, j + 1].Font.Bold = true;
                sheet1.Columns[j + 1].ColumnWidth = 15;
                myRange.Value2 = DGridEducation.Columns[j].Header;
            }
            for (int i = 0; i < DGridEducation.Columns.Count; i++)
            {
                for (int j = 0; j < DGridEducation.Items.Count; j++)
                {
                    TextBlock b = DGridEducation.Columns[i].GetCellContent(DGridEducation.Items[j]) as TextBlock;
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

        private void Search_Education_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }
    }
}
