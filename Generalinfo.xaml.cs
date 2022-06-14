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
    /// Логика взаимодействия для Generalinfo.xaml
    /// </summary>
    public partial class Generalinfo : Page
    {
        public Generalinfo()
        {
            InitializeComponent();
            DGridGeneral.ItemsSource = Personnel_DepartmentEntities.GetContext().Generalinformations.ToList();
            Update();
        }
        private void Update()
        {
            var currentgen = Personnel_DepartmentEntities.GetContext().Generalinformations.ToList();

            currentgen = currentgen.Where(p => p.FIO.ToString().ToLower().Contains(Search_General.Text.ToLower())).ToList();


            // привязка данных для отображения в DataGrid данных из таблицы БД
            DGridGeneral.ItemsSource = currentgen;
        }

        private void BtnEdit1_Click(object sender, RoutedEventArgs e)
        {
            Manager1.MainFrame1.Navigate(new AddEditPage2((sender as Button).DataContext as Generalinformation));
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var PositionForRemoving = DGridGeneral.SelectedItems.Cast<Generalinformation>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {PositionForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Personnel_DepartmentEntities.GetContext().Generalinformations.RemoveRange(PositionForRemoving);
                    Personnel_DepartmentEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    DGridGeneral.ItemsSource = Personnel_DepartmentEntities.GetContext().Generalinformations.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Manager1.MainFrame1.Navigate(new AddEditPage2(null));
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Visible = true;
            Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Microsoft.Office.Interop.Excel.Worksheet sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];

            for (int j = 0; j < DGridGeneral.Columns.Count; j++)
            {
                Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[1, j + 1];
                sheet1.Cells[1, j + 1].Font.Bold = true;
                sheet1.Columns[j + 1].ColumnWidth = 15;
                myRange.Value2 = DGridGeneral.Columns[j].Header;
            }
            for (int i = 0; i < DGridGeneral.Columns.Count; i++)
            {
                for (int j = 0; j < DGridGeneral.Items.Count; j++)
                {
                    TextBlock b = DGridGeneral.Columns[i].GetCellContent(DGridGeneral.Items[j]) as TextBlock;
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

        private void Search_General_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }
    }
}
