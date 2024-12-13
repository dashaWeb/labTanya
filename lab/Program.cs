using System;
using System.IO;

namespace lab
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Flight flight = new Flight { From = "New Delphi", Destination = "Los Angeles", Departure = new DateTime(2017, 12, 10, 7, 45,0), Number = "AC 2505", FlightDuration = new DateTime(2000,1,1,3,4,0) };
             Passenger passenger = new Passenger { Name = "John", LastName = "Doe", Birthday = new DateTime(2000, 5, 12),Phone = "+38096-458-58-96" };*/

            /*Admin admin = new Admin();
            admin.menu();*/
            Users users = new Users();
            users.menu();
        }
    }
}
