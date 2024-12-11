using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    class Admin
    {
        public List<Flight> Flights { get; } = new List<Flight>();
        public void add()
        {

        }
        public void edit(Flight flight)
        {

        }
        public void delete()
        {

        }
        public void save()
        {

        }
        public void load() { 
        }
        public Flight searchByDate(DateTime date)
        {
            return Flights.Find(fl => fl.Departure.ToLongDateString() == date.ToShortDateString());
        }
        public Flight searchByFrom(string from)
        {
            return Flights.Find(fl => fl.From.ToLower() == from.ToLower());
        }
        public Flight searchByTo(string to)
        {
            return Flights.Find(fl => fl.Destination.ToLower() == to.ToLower());
        }
        public Flight searchByName(string name)
        {
            return Flights.Find(fl => fl.Number.ToLower() == name.ToLower());
        }
        private Flight searcMenu()
        {
            Console.WriteLine(@"
            Flight search by : 
                [1] - By date;
                [2] - Place of dispatch;
                [3] - Place of arrival;
                [4] - Flight number 
                    Your choice :: 
            ");
            int choice;
            if(int.TryParse(Console.ReadLine(), out choice))
            {
                string search;
                switch (choice)
                {
                    case 1:
                        Console.Write("Enter the date in the format [dd/mm/yyyy] :: ");
                        search = Console.ReadLine();
                        return searchByDate(DateTime.Parse(search));
                    case 2:
                        Console.Write("Specify the place of departure :: ");
                        search = Console.ReadLine();
                        return searchByFrom(search);
                    case 3:
                        Console.Write("Specify the place of arrival :: ");
                        search = Console.ReadLine();
                        return searchByTo(search);
                    case 4:
                        Console.Write("Enter the flight number :: ");
                        search = Console.ReadLine();
                        return searchByName(search);
                    default:
                        throw new ArgumentException();
                }
            }
            else
            {
                throw new ArgumentException();
            }
        }
        ~Admin()
        {

        }
    }
}
