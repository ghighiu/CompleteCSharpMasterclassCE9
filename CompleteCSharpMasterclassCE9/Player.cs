using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompleteCSharpMasterclassCE9
{
    internal class Player
    {
        public char Symbol { get; set; }
        public string Name { get; set; }
        public int GamesWon { get; set; }

        public char Selection { get; set; }

        public Player()
        {
            Console.Clear();
            Console.WriteLine("TicTacToe Game");
            Console.WriteLine("");
            Console.WriteLine("Enter new players name:");
            Name = Console.ReadLine();
            Console.WriteLine("");

            GamesWon = 0;
        }

        public void ChoseSymbol()
        {
            bool validEntry = false;
            do
            {
                Console.WriteLine("");
                Console.WriteLine($"{Name}, ");
                Console.WriteLine("Choose your symbol ( X / O ): ");
                char symbolEntered = Console.ReadKey().KeyChar;

                if (symbolEntered == 'x' || symbolEntered == 'X')
                {
                    Symbol = 'X';
                    break;
                }
                if (symbolEntered == 'o' || symbolEntered == 'O')
                {
                    Symbol = 'O';
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("TicTacToe Game");
                    Console.WriteLine("");
                    Console.WriteLine("--------------------------------");
                    Console.WriteLine($"{symbolEntered} is an invalid input. Try again. ");
                    Console.WriteLine("--------------------------------");
                    Console.WriteLine("");
                }
            } while (!validEntry);

        }

        public void PlayerTurn()
        {
            Console.WriteLine(" ");
            Console.WriteLine($"{Name}, choose an empty spot (1-9) for your {Symbol}:");
            Selection = Console.ReadKey().KeyChar;
            //return Selection;
        }


    }
}
