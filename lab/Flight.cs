using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    class Flight
    {
        public string Destination { get; set; }
        public string From { get; set; }
        public string Number { get; set; }
        public DateTime Departure { get; set; }
        public List<Seat> FreeSeats { get; set; }
        public List<Seat> ReservedSeats { get;}
        public int NumberFreeSeats { get => FreeSeats.Count; }
        public DateTime FlightDuration { get; set; }
        
    }
}
