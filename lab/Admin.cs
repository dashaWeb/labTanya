using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    class Admin
    {
        public Admin()
        {
            if (File.Exists(fname))
                load();
            else
            {
                flights = new List<Flight>();
            }
        }
        private List<Flight> flights = new List<Flight>();
        public List<Flight> Flights { get => flights; }
        private string fname = @"../../../flights.json";
        public void add()
        {
            Flight flight = new Flight();
            edit(flight);
            flights.Add(flight);
        }
        public void edit(Flight flight)
        {
            Console.WriteLine("Filling in information about the flight");
            Console.Write(@"
                Enter number of flight --> ");
            flight.Number = Console.ReadLine();
            Console.Write(@"
                Enter From --> ");
            flight.From = Console.ReadLine();
            Console.Write(@"
                Enter To --> ");
            flight.Destination = Console.ReadLine();
            Console.Write(@"
                Enter the departure time of the flight in the format [dd/mm/yyyy] --> ");
            flight.Departure = DateTime.Parse(Console.ReadLine());
            Console.Write(@"
                Уnter your flight time in the format [hh:mm] --> ");
            string time = Console.ReadLine();
            flight.Departure = new DateTime(2000,01,01, int.Parse(time.Split(':')[0]), int.Parse(time.Split(':')[1]),01);
        }
        public void delete(Flight flight)
        {
            flights.Remove(flight);
        }
        public void save()
        {
            File.WriteAllText(fname, JsonConvert.SerializeObject(Flights));

        }
        public void load()
        {
            flights = JsonConvert.DeserializeObject<List<Flight>>(File.ReadAllText(fname));
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
        public Flight searchMenu()
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
            if (int.TryParse(Console.ReadLine(), out choice))
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
        public void menu()
        {
            try
            {
                int choice;
                do
                {
                    Console.WriteLine(@"
                [1] - Add flight;
                [2] - Edit flight; 
                [3] - Delete flight; 
                [4] - Sales information; 
                [5] - Free seats on the flight; 
                [0] - Exit
                    Your choice :: 
            ");
                    if (int.TryParse(Console.ReadLine(), out choice))
                    {
                        switch (choice)
                        {
                            case 1:
                                add();
                                break;
                            case 2:
                                while (true)
                                {
                                    try
                                    {
                                        var find = searchMenu();
                                        edit(find);
                                        break;
                                    }
                                    catch (ArgumentException ex)
                                    {
                                        error();
                                    }
                                }
                                break;
                            case 3:
                                while (true)
                                {
                                    try
                                    {
                                        var find = searchMenu();
                                        delete(find);
                                        break;
                                    }
                                    catch (ArgumentException ex)
                                    {
                                        error();
                                    }
                                }
                                break;
                            case 4:
                                while (true)
                                {
                                    try
                                    {
                                        var find = searchMenu();
                                        find.print(find.FreeSeats);
                                        break;
                                    }
                                    catch (ArgumentException ex)
                                    {
                                        error();
                                    }
                                }
                                break;
                            case 5:
                                while (true)
                                {
                                    try
                                    {
                                        var find = searchMenu();
                                        find.print(find.ReservedSeats);
                                        break;
                                    }
                                    catch (ArgumentException ex)
                                    {
                                        error();
                                    }
                                }
                                break;
                        }
                    }
                    else
                    {
                        error();
                    }
                } while (choice != 0);
            }
            catch (Exception)
            {
                error();
            }
            save();
        }
        private void error()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error!!! Try again!");
            Console.ResetColor();
        }

    }
}
