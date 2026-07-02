using System;
using System.Windows;

namespace ExamApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Flight> Data { get; set; } = new List<Flight>();
        private FlightControl Controls { get; set; } = new FlightControl();

        private const string FILE_NAME = "data.txt";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void UpdateData()
        {
            var stringData = Controls.ToStringArray(Data);
            dataView.ItemsSource = stringData;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new InputDialogWindow(new Flight());
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                Data.Add(dialog.FlightData);
                UpdateData();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Data.Count == 0)
            {
                ShowErrorMessage("Добавьте хотя бы одну запись.");
                return;
            }

            Controls.SortByHourAndMinutesArrive(Data);
            UpdateData();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (Data.Count == 0)
            {
                ShowErrorMessage("Добавьте хотя бы одну запись.");
                return;
            }
            try
            {
                Controls.SaveToFile(Data, FILE_NAME);
                MessageBox.Show($"Записи были успешно сохранены в файл {FILE_NAME} в количестве {Data.Count} шт.", "Успешное сохранение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch(Exception exception) 
            {
                MessageBox.Show(exception.Message, "Ошибка при сохранении файла", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}