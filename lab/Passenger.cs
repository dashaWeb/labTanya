using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    class Passenger
    {
        public string  Name { get; set; }
        public string  LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Phone { get; set; }
        public Card Card { get; set; }
        public Ticket Ticket { get; set; }
        public override string ToString()
        {
            return @$"
            {"Name of passenger:",-30}{"Birthday:",-20}{"Phone:",-20}
            {Name+ " " + LastName,-30}{Birthday.ToShortDateString(),-20}{Phone,-20}";
        }

    }
}
