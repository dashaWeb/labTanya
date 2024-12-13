using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    class Flight
    {
        public Flight()
        {
            GenerateSeats();
        }
        public string Destination { get; set; }
        public string From { get; set; }
        public string Number { get; set; }
        public DateTime Departure { get; set; }
        public DateTime FlightDuration { get; set; }
        public List<Seat> FreeSeats { get;} = new List<Seat>();
        public List<Seat> ReservedSeats { get; } = new List<Seat>();
        public int NumberFreeSeats { get => FreeSeats.Count; }
        private void GenerateSeats()
        {
            var flights = Enum.GetValues(typeof(Service));
            foreach (Service item in flights)
            {
                for (int i = 0; i < (int)item; i++)
                {
                    FreeSeats.Add(new Seat(item));
                }
            }
        }
        public void print(List<Seat> seats)
        {
            foreach (var item in seats)
            {
                Console.WriteLine(item);
            }
        }
        public override string ToString()
        {
            return $"{Number} {From} {Destination} {Departure.ToShortDateString()} {FlightDuration.ToShortTimeString()} {NumberFreeSeats}";
        }
    }
}
