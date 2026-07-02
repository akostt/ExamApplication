using System.Windows;

namespace ExamApplication
{
    /// <summary>
    /// Логика взаимодействия для InputDialogWindow.xaml
    /// </summary>
    public partial class InputDialogWindow : Window
    {
        public Flight FlightData { get; private set; }

        public InputDialogWindow(Flight flight, bool edit = false)
        {
            this.FlightData = flight;
            if (edit)
            {
                AeroportOutInput.Text = FlightData.AeroportOut;
                AeroportInInput.Text = FlightData.AeroportIn;
                TimeArriveInput.Text = $"{FlightData.HourArrive}:{FlightData.MinutesArrive}";
            }
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(AeroportOutInput.Text))
            {
                ShowErrorMessage("Значение аэропорта отправления пустое.");
                return;
            }

            if (string.IsNullOrEmpty(AeroportInInput.Text))
            {
                ShowErrorMessage("Значение аэропорта прибытия пустое.");
                return;
            }

            if (!TimeOnly.TryParse(TimeArriveInput.Text, out var timeArrive))
            {
                ShowErrorMessage("Не удалось распознать введенное время отправления.");
                return;
            }

            FlightData.AeroportOut = AeroportOutInput.Text;
            FlightData.AeroportIn = AeroportInInput.Text;
            FlightData.HourArrive = timeArrive.Hour;
            FlightData.MinutesArrive = timeArrive.Minute;
            DialogResult = true;
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
