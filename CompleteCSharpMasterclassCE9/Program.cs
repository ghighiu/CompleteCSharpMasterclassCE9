using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CompleteCSharpMasterclassCE9
{
    internal class Program
    {
        public static readonly int SymbolXcode = 1;
        public static readonly int SymbolOcode = 2;
        public static readonly int FieldStartingNumber = 1;
        public static readonly int FieldNumbersInARowMin = 3;
        public static readonly int FieldNumbersInARowMax = 12;
        public static int FieldNumbersInARow = 3;
        public static int FieldEndingNumber = 9;
        public static int GamesPlayed = 0;
        public static int CurrentPlayer = 1;
        public static int CurrentTurnNumber = 0;
        public static bool IsWinner;
        public static bool Continue;

        static void Main(string[] args)
        {

            InsertNameHeader();

            //Choose the field size
            string message = $"Choose a size for the game field ({FieldNumbersInARowMin} - {FieldNumbersInARowMax}):";
            InputNumberRange(message, FieldNumbersInARowMin, FieldNumbersInARowMax, out int fieldNumber);
            FieldNumbersInARow = fieldNumber;
            FieldEndingNumber = (int)(fieldNumber * fieldNumber);

            InsertNameHeader();

            // Player 1 instantiate
            var player1 = new Player(CurrentPlayer);
            player1.ChooseSymbol();
            CurrentPlayer++;

            //Player 2 instantiate
            var player2 = new Player(CurrentPlayer);
            if (player1.Symbol == SymbolXcode) player2.Symbol = SymbolOcode;
            else player2.Symbol = SymbolXcode;
            CurrentPlayer--;

            while (true)
            {
                //Create the field
                Field fieldGame = new Field();
                string fieldString = SetField(FieldStartingNumber, FieldNumbersInARow);
                Console.WriteLine(fieldString);

                //Player turn
                for (int i = FieldStartingNumber; i <= FieldEndingNumber; i++)
                {
                    PlayerSwitch(player1, player2, fieldGame, ref fieldString);
                    if (IsWinner)
                    {
                        DisplayWinner(player1, player2);
                        break;
                    }
                }

                if (!IsWinner)
                {
                    InsertNameHeader();
                    Console.WriteLine("It was a draw.");
                }

                GamesPlayed++;
                AskContinue(player1,player2);

                if (!Continue) break;
            }
        }

        private static void AskContinue(Player player1, Player player2)
        {
            InsertEmptyRowInConsole();
            Console.WriteLine($"Total games played: {GamesPlayed}");
            InsertEmptyRowInConsole();
            Console.WriteLine($"{player1.Name} won {player1.GamesWon} games.");
            Console.WriteLine($"{player2.Name} won {player2.GamesWon} games.");
        }

        private static void DisplayWinner(Player player1, Player player2)
        {
            InsertNameHeader();

            if (CurrentPlayer == 1) Console.WriteLine($"{player1.Name} won this game!");
            if (CurrentPlayer == 2) Console.WriteLine($"{player2.Name} won this game!");

            InsertEmptyRowInConsole();
        }

        private static void PlayerSwitch(Player player1, Player player2, Field field, ref string fieldString)
        {
            if (CurrentPlayer == 1)
            {
                PlayerAction(player1, field, ref fieldString);
                if (StopGame()) return;
                CurrentPlayer = 2;
            }
            if (CurrentPlayer == 2)
            {
                PlayerAction(player2, field, ref fieldString);
                if (StopGame()) return;
                CurrentPlayer = 1;
            }
        }

        private static bool StopGame()
        {
            if (IsWinner) return true;
            if (CurrentTurnNumber == FieldEndingNumber) return true;
            return false;
        }

        private static void PlayerAction(Player player, Field field, ref string fieldString)
        {
            IsWinner = false;
            bool isValid = false;
            int positionColumn = 0;
            int positionRow = 0;

            while (!isValid)
            {
                player.PlayerTurn();
                field.TransformPosition(player.Selection, out positionColumn, out positionRow);
                isValid = field.ValidateInput(positionColumn, positionRow);
            }
            field.ModifyEntry(positionColumn, positionRow, player.Symbol);
            fieldString = ModifyFieldString(player.Selection, player.Symbol, ref fieldString);
            CurrentTurnNumber++;
            InsertNameHeader();
            Console.WriteLine(fieldString);
            if (CurrentTurnNumber >= (FieldNumbersInARow * 2) - 1) IsWinner = field.CheckWinner(positionColumn, positionRow, player.Symbol);
            if (IsWinner) player.GamesWon++;
        }

        private static string ModifyFieldString(int selection, int symbol, ref string fieldString)
        {
            byte digits = (byte)Math.Floor(Math.Log10(selection) + 1);
            string symbolString = "";
            string oldValueString;
            string newValueString;
            if (symbol == SymbolXcode) symbolString = "X";
            if (symbol == SymbolOcode) symbolString = "O";

            if (digits == 1)
            {
                oldValueString = string.Format("  {0} ", selection.ToString());
                newValueString = string.Format("  {0} ", symbolString);
                fieldString = fieldString.Replace(oldValueString, newValueString);
            }
                
            if (digits == 2)
            {
                oldValueString = string.Format(" {0} ", selection.ToString());
                newValueString = string.Format("  {0} ", symbolString);
                fieldString = fieldString.Replace(oldValueString, newValueString);
            }
            if (digits == 3)
            {
                oldValueString = string.Format("{0} ", selection.ToString());
                newValueString = string.Format("  {0} ", symbolString);
                fieldString = fieldString.Replace(oldValueString, newValueString);
            }
            return fieldString;
            
        }

        private static void InsertNameHeader()
        {
            Console.Clear();
            InsertEmptyRowInConsole();
            Console.WriteLine("TicTacToe Game");
            InsertEmptyRowInConsole();
        }

        public static void InputNumberRange(string message, int min, int max, out int numberInput)
        {
            InsertEmptyRowInConsole();
            Console.WriteLine(message);
            while (true)
            {
                string? numberInputString = Console.ReadLine();
                if (int.TryParse(numberInputString, out numberInput))
                {
                    if (numberInput >= min && numberInput <= max)
                    {
                        return;
                    }
                    else Console.WriteLine($"Input out of range. Choose a number between {min} and {max}: ");
                }
                else Console.WriteLine($"Invalid input! Enter a number between {min} and {max}: ");
            }

        }

        public static void InsertEmptyRowInConsole()
        {
            Console.WriteLine("");
        }

        public static string SetField(int min, int fieldSize)
        {
            InsertNameHeader();
            StringBuilder stringDivider = new StringBuilder();
            int currentPosition = min;
            byte digits;
            for (int y = min; y <= fieldSize; y++)
            {
                for (int x = min; x < fieldSize; x++)
                {
                    stringDivider.Append("     |");
                }
                stringDivider.Append("      ");
                stringDivider.Append("\n");
                for (int x = min; x < fieldSize; x++)
                {
                    digits = (byte)Math.Floor(Math.Log10(currentPosition) + 1);
                    if (digits == 1) stringDivider.Append($"   {currentPosition} |");
                    if (digits == 2) stringDivider.Append($"  {currentPosition} |");
                    if (digits == 3) stringDivider.Append($" {currentPosition} |");
                    currentPosition++;
                }
                digits = (byte)Math.Floor(Math.Log10(currentPosition) + 1);
                if (digits == 1) stringDivider.Append($"   {currentPosition} ");
                if (digits == 2) stringDivider.Append($"  {currentPosition} ");
                if (digits == 3) stringDivider.Append($" {currentPosition} ");
                currentPosition++;
                stringDivider.Append("\n");
                for (int x = min; x < fieldSize; x++)
                {
                    stringDivider.Append("_____|");
                }
                stringDivider.Append("_____");
                stringDivider.Append("\n");
            }
            stringDivider.Append("      ");
            stringDivider.Append("\n");
            stringDivider.Append("\n");
            stringDivider.Append("\n");

            return stringDivider.ToString();
        }
    }
}