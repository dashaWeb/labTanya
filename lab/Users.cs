using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    class Users
    {
        public Users()
        {
            if (File.Exists(fname))
                load();
        }
        private string fname = @"../../../passengers.json";
        private List<Passenger> passengers = new List<Passenger>();
        public List<Passenger> Passengers { get => passengers; }
        private void addPassenger()
        {
            Passenger passenger = new Passenger();
            editPassenger(passenger);
            Passengers.Add(passenger);
        }
        private void editPassenger(Passenger passenger)
        {
            string addCard;
            Console.WriteLine("Filling in information about the passenger");
            Console.Write(@"
                Enter name --> ");
            passenger.Name = Console.ReadLine();
            Console.Write(@"
                Enter last name --> ");
            passenger.LastName = Console.ReadLine();
            Console.Write(@"
                Enter the phone in the format [+380XX-XXX-XX-XX] --> ");
            passenger.Phone = Console.ReadLine();
            Console.Write(@"
                Enter the date in the format [dd/mm/yyyy] --> ");
            passenger.Birthday = DateTime.Parse(Console.ReadLine());

            if (passenger.Card != null)
            {
                Console.WriteLine("Edit card [yes/no]");
                addCard = Console.ReadLine();
                if (addCard.ToLower() == "no")
                    return;
                passenger.Card.EditCard();
            }
            else
            {
                Console.WriteLine("You don't have a card! Add a new card [yes/no]");
                addCard = Console.ReadLine();
                if (addCard.ToLower() == "no")
                    return;
                Card card = new Card();
                card.EditCard();
                passenger.Card = card;
            }
        }
        private Passenger searchPassenger()
        {
            Console.WriteLine(@"
            Passenger search by : 
                [1] - By name;
                [2] - By phone; 
                    Your choice :: 
            ");
            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                string search;
                switch (choice)
                {
                    case 1:
                        Console.Write("Enter the name in the format [name lastname] :: ");
                        search = Console.ReadLine();
                        var name = search.Split(' ');
                        return searchByName(name[0], name[1]);
                    case 2:
                        Console.Write("Enter the phone in the format [+380XX-XXX-XX-XX] :: ");
                        search = Console.ReadLine();
                        return searchByPhone(search);
                    default:
                        throw new ArgumentException();
                }
            }
            else
            {
                throw new ArgumentException();
            }
        }
        private Passenger searchByName(string name, string lastname)
        {
            return Passengers.Find(pass => pass.Name.ToLower() == name.ToLower() && pass.LastName.ToLower() == lastname);
        }
        private Passenger searchByPhone(string phone)
        {
            return Passengers.Find(pass => pass.Phone.ToLower() == phone.ToLower());
        }
        public void menu()
        {
            try
            {
                int choice;
                do
                {
                    Console.WriteLine(@"
                [1] - Add passenger;
                [2] - Edit passenger; 
                [3] - Find a flight
                [0] - Exit
                    Your choice :: 
            ");
                    if (int.TryParse(Console.ReadLine(), out choice))
                    {
                        switch (choice)
                        {
                            case 1:
                                addPassenger();
                                break;
                            case 2:
                                while (true)
                                {
                                    try
                                    {
                                        var find = searchPassenger();
                                        editPassenger(find);
                                        break;
                                    }
                                    catch (ArgumentException ex)
                                    {
                                        error();
                                    }
                                }
                                break;
                            case 3:
                                order();
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
        private void load()
        {
            passengers = JsonConvert.DeserializeObject<List<Passenger>>(File.ReadAllText(fname));

        }
        public void save()
        {
            File.WriteAllText(fname, JsonConvert.SerializeObject(Passengers));
        }
        private void error()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error!!! Try again!");
            Console.ResetColor();
        }
        public void print()
        {
            foreach (var item in passengers)
            {
                int i = 0;
                Console.WriteLine($"#{++i}\n{item}");
            }
        }
        private void order()
        {
            Admin admin = new Admin();
            if (admin.Flights.Count == 0)
            {
                Console.WriteLine("The flight list is empty");
                return;
            }
            Flight flight = admin.searchMenu();
            if (flight == null)
            {
                Console.WriteLine("The flight not found");
                return;
            }
            Console.WriteLine("Choose a class ");
            int i = 0;
            foreach (var item in Enum.GetNames(typeof(Service)))
            {
                Console.WriteLine($"\t [{++i}] - {item} ");
            }
            i = int.Parse(Console.ReadLine());
            var selectSeats = flight.FreeSeats.FindAll(e => e.Service == (Service)Enum.Parse(typeof(Service), Enum.GetNames(typeof(Service))[i - 1]));
            flight.print(selectSeats);
            Console.WriteLine("Choose a number seat ");
            string number = Console.ReadLine();
            var seat = selectSeats.Find(e => e.Number == number);
            Console.WriteLine("Book a ticket [yes/no]");
            number = Console.ReadLine();
            if (number.ToLower() == "no")
                return;
            Console.WriteLine(@"Choose a payment method [1] - cash; [2] - card");
            i = int.Parse(Console.ReadLine());
            if(i != 1 || i != 2)
            {
                error();
                return;
            }

        }
    }
}
