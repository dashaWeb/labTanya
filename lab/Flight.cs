using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    class Flight
    {
        public Flight()
        {
        }
        public string Destination { get; set; }
        public string From { get; set; }
        public string Number { get; set; }
        public DateTime Departure { get; set; }
        public DateTime FlightDuration { get; set; }
        public List<Seat> FreeSeats { get; set; } = new List<Seat>(260);
        public List<Seat> ReservedSeats { get; set; } = new List<Seat>();
        public int NumberFreeSeats { get => FreeSeats.Count; }
        public void GenerateSeats()
        {
            var flights = Enum.GetValues(typeof(Service));
            int j = 0;
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
            return $"{Number} {From} {Destination} {Departure.ToShortDateString()} {FlightDuration.ToShortTimeString()} seats :: {NumberFreeSeats}";
        }
        public Seat getFreeSeat()
        {
            try
            {
                Console.WriteLine("Choose a class ");
                int i = 0;
                foreach (var item in Enum.GetNames(typeof(Service)))
                {
                    Console.WriteLine($"\t [{++i}] - {item} ");
                }
                i = int.Parse(Console.ReadLine());
                var selectSeats = FreeSeats.FindAll(e => e.Service == (Service)Enum.Parse(typeof(Service), Enum.GetNames(typeof(Service))[i - 1]));
                print(selectSeats);
                Console.WriteLine("Choose a number seat ");
                string number = Console.ReadLine();
                return selectSeats.Find(e => String.Compare(e.Number,number,true) == 0);
            }
            catch (Exception ex)
            {
                error();
            }
            return null;
        }
        public void printRecerved()
        {
            Console.WriteLine($"{Number} {From} {Destination} {Departure.ToShortDateString()} {FlightDuration.ToShortTimeString()} Number of sale seats :: {ReservedSeats.Count}");
        }
        private void error()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error!!! Seat not found.  Try again!");
            Console.ResetColor();
        }
    }
}
