using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace lab
{
    class Program
    {
        static void Main(string[] args)
        {
            Admin admin = new Admin();
            Users users = new Users();

            while (true)
            {
               
                try
                {
                    Console.Write(@"
                     Select the operating mode
                         [1] - admin;
                         [2] - user;
                         [0] - exit;
                               --> ");
                    int choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            admin.menu();
                            break;
                        case 2:
                            users.menu(admin);
                            break;
                        case 0:
                            return;
                    }
                }
                catch (Exception)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error! Try again");
                    Console.ResetColor();
                }
            }

        }
    }
}
