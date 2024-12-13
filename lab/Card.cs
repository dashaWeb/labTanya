using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    class Card
    {
        public string Number { get; set; }
        public string CVV { get; set; }
        public DateTime Term { get; set; }
        public double Money { get; set; }
        public void EditCard()
        {
            Console.Write(@"
                Enter Number --> ");
            Number = Console.ReadLine();
            Console.Write(@"
                Enter CVV --> ");
            CVV = Console.ReadLine();
            Console.WriteLine(@"
                Enter the date in the format [mm/yyyy] --> ");
            Term = DateTime.Parse("10/" + Console.ReadLine());
            Console.Write(@"
                Enter Money --> ");
            Money = Double.Parse(Console.ReadLine());
        }
        public override string ToString()
        {
            return $"Number :: {Number, -10} CVV :: {CVV,-10} Term :: {Term.Month}/{Term.Day,-10} Money :: {Money}";
        }
    }
}
