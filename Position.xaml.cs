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
    /// Логика взаимодействия для Position.xaml
    /// </summary>
    public partial class Position : Page
    {
        public Position()
        {
            
            InitializeComponent();
            DGridPosition.ItemsSource = Personnel_DepartmentEntities.GetContext().Positioninformations.ToList();
        }

        private void BtnAdd4_Click(object sender, RoutedEventArgs e)
        {
            Manager1.MainFrame1.Navigate(new AddEditPage1(null)); 
        }

        private void BtnDelite4_Click(object sender, RoutedEventArgs e)
        {
            var PositionForRemoving = DGridPosition.SelectedItems.Cast<Positioninformation>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {PositionForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Personnel_DepartmentEntities.GetContext().Positioninformations.RemoveRange(PositionForRemoving);
                    Personnel_DepartmentEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    DGridPosition.ItemsSource = Personnel_DepartmentEntities.GetContext().Positioninformations.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void BtnEdit4_Click(object sender, RoutedEventArgs e)
        {
            Manager1.MainFrame1.Navigate(new AddEditPage1((sender as Button).DataContext as Positioninformation));
        }

        private void Exp_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Visible = true;
            Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Microsoft.Office.Interop.Excel.Worksheet sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];

            for (int j = 0; j < DGridPosition.Columns.Count; j++)
            {
                Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[1, j + 1];
                sheet1.Cells[1, j + 1].Font.Bold = true;
                sheet1.Columns[j + 1].ColumnWidth = 15;
                myRange.Value2 = DGridPosition.Columns[j].Header;
            }
            for (int i = 0; i < DGridPosition.Columns.Count; i++)
            {
                for (int j = 0; j < DGridPosition.Items.Count; j++)
                {
                    TextBlock b = DGridPosition.Columns[i].GetCellContent(DGridPosition.Items[j]) as TextBlock;
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
    }
}
