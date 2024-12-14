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
            Service = service_;
            SetNumber();
            SetPrice();
        }
        public Seat()
        {

        }

        public Service Service { get; set; }
        public double Price { get; set; }
        public string Number { get; set; }
        //public Passenger Passenger { get; set; }
        private void SetPrice()
        {
            switch (Service)
            {
                case Service.ECONOMY:
                    Price = 500;
                    break;
                case Service.BUSINESS:
                    Price = 1000;
                    break;
                case Service.FIRST_CLASS:
                    Price = 1500;
                    break;
            }
        }
        private void SetNumber()
        {
            Number = (char)new Random().Next(65, 91) + new Random().Next(1000).ToString();
        }
        public override string ToString()
        {
            return $"{Service,-20} {Number,-10} {Price}$";
        }
    }
}
