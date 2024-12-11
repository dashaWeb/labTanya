using System;

namespace lab
{
    class Program
    {
        static void Main(string[] args)
        {
            Flight flight = new Flight { From = "New Delphi", Destination = "Los Angeles", Departure = new DateTime(2017, 12, 10, 7, 45,0), Number = "AC 25 05" };
            Passenger passenger = new Passenger { Name = "John", LastName = "Doe", Birthday = new DateTime(2000, 5, 12),Phone = "+38096-458-58-96" };
            
            Console.WriteLine(passenger);
            
        }
    }
}
