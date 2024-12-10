using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    class Ticket
    {
        public Flight Flight { get; set; }
        public Passenger Passenger { get; set; }
        public Seat Seat { get; set; }
        public override string ToString()
        {
            return @$"
            {"Name of passenger:",-30}{"Flight №:",-30}{"Class:",-20}
            {Passenger.Name + " " + Passenger.LastName,-30}{Flight.Number,-30}{Enum.GetName(typeof(Service), Seat.Service),-20}
            
            {"From: " + Flight.From,-30}{"Date:",-30}{"Seat :",-20}
            {"To:" + Flight.Destination,-30}{Flight.Departure.ToShortDateString(),-30}{Seat.Number,-20}
                         
                                          BOARDING TIME
                                              {Flight.Departure.ToLongTimeString()}";
        }
    }
}
