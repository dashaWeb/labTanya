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
                Console.Write("Edit card [yes/no] --> ");
                addCard = Console.ReadLine();
                if (addCard.ToLower() == "no")
                    return;
                Card card = new Card();
                card.EditCard();
                passenger.Card = card;
            }
            else
            {
                Console.Write("You don't have a card! Add a new card [yes/no] --> ");
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
            Console.Write(@"
            Passenger search by : 
                [1] - By name;
                [2] - By phone; 
                    Your choice :: ");
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
                        Console.WriteLine(name[0] + " " + name[1]);
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
            return Passengers.Find(pass => String.Compare(pass.Name, name, true) == 0 && String.Compare(pass.LastName, lastname, true) == 0);
        }
        private Passenger searchByPhone(string phone)
        {
            return Passengers.Find(pass => pass.Phone.ToLower() == phone.ToLower());
        }
        public void menu(Admin admin)
        {
            try
            {
                int choice;
                do
                {
                    Console.Write(@"
                [1] - Add passenger;
                [2] - Edit passenger; 
                [3] - Find a flight
                [4] - Print all passengers
                [0] - Exit
                    Your choice :: ");
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
                                order(admin);
                                break;
                            case 4:
                                print();
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
            int i = 0;
            foreach (var item in passengers)
            {
                Console.WriteLine($"#{++i}\n{item}");
            }
        }
        private void order(Admin admin)
        {
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
            int i;
            while (true)
            {
                try
                {
                    Console.Write("Enter the number of seats --> ");
                    i = int.Parse(Console.ReadLine());
                    break;
                }
                catch (Exception)
                {
                }
            }
            string choice;
            for (int j = 0; j < i; j++)
            {
                var seat = flight.getFreeSeat();
                if (seat == null)
                {
                    j--;
                    continue;
                }
                Console.WriteLine("Book a ticket [yes/no]");
                choice = Console.ReadLine();
                if (choice.ToLower() == "no")
                {
                    break;
                }

                Passenger passenger;
                while (true)
                {
                    passenger = selectPassenger();
                    if (passenger == null)
                    {
                        Console.WriteLine("Error passenger. Try again");
                    }
                    else
                    {
                        break;
                    }
                }
                bool res = payment(passenger, seat);
                if (!res)
                {
                    j--;
                    continue;
                }
                var ticket = new Ticket { FlightDeparture = flight.Departure, FlightDestination = flight.Destination, FlightFrom = flight.From, FlightNumber = flight.Number, PassengerName = passenger.Name, Seat = seat, PassengerLastName = passenger.LastName };
                passenger.Ticket = ticket;
                flight.FreeSeats.Remove(seat);
                Console.WriteLine(flight.FreeSeats.Count + "  " + flight.NumberFreeSeats);
                Console.WriteLine(admin.Flights[0].NumberFreeSeats);
                flight.ReservedSeats.Add(seat);
                Console.BackgroundColor = ConsoleColor.Green;
                Console.WriteLine("The ticket has been sent");
                Console.ResetColor();
                Console.WriteLine(ticket);
            }
        }
        private Passenger selectPassenger()
        {
            int i;
            Console.Write(@"
                    [1] - Select passenger;
                    [2] - Add new passenger 
                                --> ");
            i = int.Parse(Console.ReadLine());
            switch (i)
            {
                case 1:
                    return searchPassenger();
                case 2:
                    var pass = new Passenger();
                    editPassenger(pass);
                    passengers.Add(pass);
                    return pass;
                default:
                    return null;
            }
        }
        private bool payment(Passenger pass, Seat seat)
        {
            while (true)
            {
                Console.Write(@"Choose a payment method [1] - cash; [2] - card; [0] - cancel  --> ");
                int i = int.Parse(Console.ReadLine());
                if (i == 0)
                    return false;
                if (i != 1 && i != 2)
                {
                    Console.WriteLine("Error! Make the right choice");
                    continue;
                }

                switch (i)
                {
                    case 1:
                        Console.Write("Enter the amount of money -->");
                        double cash = Double.Parse(Console.ReadLine());
                        double remainder = cash - seat.Price;
                        if (remainder < 0)
                        {
                            Console.WriteLine("Insufficient funds");
                            continue;
                        }
                        Console.WriteLine($"Payment successful.The rest of you {remainder}$");
                        break;
                    case 2:
                        if (pass.Card == null)
                        {
                            Console.WriteLine("You don't have a card. Add now? [yes/no]");
                            string choice = Console.ReadLine();
                            if (choice == "yes")
                            {
                                pass.Card = new Card();
                                pass.Card.EditCard();
                            }
                            else
                            {
                                Console.WriteLine("Choose another payment method");
                                continue;
                            }
                        }
                        remainder = pass.Card.Money - seat.Price;
                        if (remainder < 0)
                        {
                            Console.WriteLine("Insufficient funds");
                            continue;
                        }
                        pass.Card.Money -= seat.Price;
                        Console.WriteLine($"Payment successful.");
                        break;
                }
                return true;
            }

        }
    }
}
