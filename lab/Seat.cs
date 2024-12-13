using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    enum Service
    {
        ECONOMY = 200, BUSINESS = 50, FIRST_CLASS = 10
    }
    class Seat
    {
        public Seat(Service service_)
        {
            service = service_;
            SetNumber();
            SetPrice();
        }
        private double price = 0;
        private string number;
        private Service service;
        public Service Service { get=>service; }
        public double Price { get=>price; }
        public string Number { get=>number; }
        //public Passenger Passenger { get; set; }
        private void SetPrice()
        {
            switch (Service)
            {
                case Service.ECONOMY:
                    price = 500;
                    break;
                case Service.BUSINESS:
                    price = 1000;
                    break;
                case Service.FIRST_CLASS:
                    price = 1500;
                    break;
            }
        }
        private void SetNumber()
        {
            number = (char)new Random().Next(65, 91) + new Random().Next(1000).ToString();
        }
        public override string ToString()
        {
            return $"{Service,-20} {number,-10} {Price}$";
        }
    }
}
