using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CompleteCSharpMasterclassCE9
{
    internal class Player
    {
        public int Symbol { get; set; }
        public string Name { get; set; }
        public int GamesWon { get; set; }
        public int Selection { get; set; }

        

        public Player(int number)
        {
            Program.InsertEmptyRowInConsole();
            Console.WriteLine($"Player {number}, enter your name:");
            while (true)
            {
                string? name = Console.ReadLine();
                if (!string.IsNullOrEmpty(name))
                {
                    Name = name;
                    break;
                }
                else
                {
                    Program.InsertEmptyRowInConsole();
                    Console.WriteLine("Invalid input. Try again:");
                }
            }
            Program.InsertEmptyRowInConsole();
            GamesWon = 0;
        }

        public void ChooseSymbol()
        {
            Program.InsertEmptyRowInConsole();
            Console.WriteLine($"{Name}, choose your symbol ( X / O ): ");
            while (true)
            {        
                string? symbolEntered = Console.ReadLine();
                if (!string.IsNullOrEmpty(symbolEntered))
                {
                    if (symbolEntered.ToUpper().Equals("X"))
                    {
                        Symbol = Program.SymbolXcode;
                        break;
                    }

                    if (symbolEntered.ToUpper().Equals("O"))
                    {
                        Symbol = Program.SymbolOcode;
                        break;
                    }
                    else
                    {
                        Program.InsertEmptyRowInConsole();
                        Console.WriteLine($"Symbol, {symbolEntered}, is not a invalid input. Try again ( X / O ): ");
                    }
                }
                else
                {
                    Program.InsertEmptyRowInConsole();
                    Console.WriteLine($"Invalid input. Try again ( X / O ): ");
                }
            }
        }

        public void PlayerTurn()
        {
            Program.InsertEmptyRowInConsole();
            StringBuilder stringChooseSpot = new StringBuilder();
            stringChooseSpot.Append($"{Name}, choose an empty spot ({Program.FieldStartingNumber}-{Program.FieldEndingNumber}) for your ");
            if (Symbol == Program.SymbolXcode) stringChooseSpot.Append("X: ");
            if (Symbol == Program.SymbolOcode) stringChooseSpot.Append("O: ");

            int selection;
            Program.InputNumberRange(stringChooseSpot.ToString(), Program.FieldStartingNumber, Program.FieldEndingNumber, out selection);
            Selection = selection;
        }
    }
}
