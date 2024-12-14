using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    class Ticket
    {
        public string PassengerName { get; set; }
        public string PassengerLastName { get; set; }
        public string FlightFrom { get; set; }
        public string FlightNumber { get; set; }
        public string FlightDestination { get; set; }
        public DateTime FlightDeparture { get; set; }

        public Seat Seat { get; set; }
        public override string ToString()
        {
            return @$"
            {"Name of passenger:",-30}{"Flight №:",-30}{"Class:",-20}
            {PassengerName + " " + PassengerLastName,-30}{FlightNumber,-30}{Enum.GetName(typeof(Service), Seat.Service),-20}
            
            {"From: " + FlightFrom,-30}{"Date:",-30}{"Seat :",-20}
            {"To:" + FlightDestination,-30}{FlightDeparture.ToShortDateString(),-30}{Seat.Number,-20}
                         
                                          BOARDING TIME
                                              {FlightDeparture.ToShortTimeString()}";
        }
    }
}
