using System.IO;

namespace ExamApplication
{
    class FlightControl
    {
        public void SortByHourAndMinutesArrive(List<Flight> flights)
        {
            for (int i = 0; i < flights.Count - 1; i++)
            {
                for (int j = 0; j < flights.Count - i - 1; j++)
                {
                    Flight flight1 = flights[j];
                    Flight flight2 = flights[j + 1];

                    if (flight1.HourArrive > flight2.HourArrive ||
                       (flight1.HourArrive == flight2.HourArrive && flight1.MinutesArrive > flight2.MinutesArrive))
                    {
                        flights[j] = flight2;
                        flights[j + 1] = flight1;
                    }
                }
            }
        }

        public void SaveToFile(List<Flight> flights, string fileName)
        {
            var stringData = ToStringArray(flights);
            File.WriteAllText(fileName, string.Join("\n\n", stringData));
        }

        public string[] ToStringArray(List<Flight> flights)
        {
            int number = 1;
            var result = flights
                .Select(flight => $"Самолёт №{number++}\nАэропорт отправления: {flight.AeroportOut}\nАэропорт прибытия: {flight.AeroportIn}\nВремя отправления: {flight.HourArrive}:{flight.MinutesArrive.ToString("00")}");
            return result.ToArray();
        }
    }
}
