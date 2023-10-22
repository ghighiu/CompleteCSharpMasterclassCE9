using System.ComponentModel.DataAnnotations;

namespace CompleteCSharpMasterclassCE9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int gamesPlayed = 1;
            bool continuePlaying = false;

            var player1 = new Player();
            player1.ChoseSymbol();
            var player2 = new Player();

            if (player1.Symbol == 'X') player2.Symbol = 'O';
            else player2.Symbol = 'X';

            do
            {
                var field = new Field();
                const byte totalInputs = 9;
                     
                Console.Clear();
                Console.WriteLine("TicTacToe Game");
                Console.WriteLine("");
                                          
                byte playerNumberTurn = 1;
                bool isWinner = false;

                for (int i = 0; i <= totalInputs-1; i++)
                {
                    SetField(field.FieldArray);
                    switch (playerNumberTurn)
                    {
                        case 1:
                            bool positionIsValid = false;
                            do
                            {
                                player1.PlayerTurn();
                                positionIsValid = field.ValidateInput(player1.Selection);
                            } while (!positionIsValid);
                            field.ModifyEntry(player1.Selection, player1.Symbol);
                            isWinner = field.CheckWin(player1.Symbol);
                            if (isWinner)
                            {
                                DisplayWinner(player1.Name, player1.Symbol);
                                player1.GamesWon++;
                            }      
                            playerNumberTurn = 2;
                            break;
                        case 2:
                            positionIsValid = false;
                            do
                            {
                                player2.PlayerTurn();
                                positionIsValid = field.ValidateInput(player2.Selection);
                            } while (!positionIsValid);
                            field.ModifyEntry(player2.Selection, player2.Symbol);
                            isWinner = field.CheckWin(player2.Symbol);
                            if (isWinner)
                            {
                                DisplayWinner(player2.Name, player2.Symbol);
                                player2.GamesWon++;
                            }
                            playerNumberTurn = 1;
                            break;

                        default:
                            break;
                    }
                    if (isWinner) break;
                }
                
                if (!isWinner)
                {
                    Console.Clear();
                    Console.WriteLine("TicTacToe Game"); 
                    Console.WriteLine("");
                    Console.WriteLine($"The {gamesPlayed} game is a draw!");
                }

                Console.WriteLine("");
                Console.WriteLine($"Number of games played: {gamesPlayed}.");
                Console.WriteLine($"Player {player1.Name}, won {player1.GamesWon} games.");
                Console.WriteLine($"Player {player2.Name}, won {player2.GamesWon} games.");

                continuePlaying = ContinuePlaying();            
                gamesPlayed++;

            } while (continuePlaying);


        }

        private static void DisplayWinner(string name, char symbol)
        {
            Console.Clear();
            Console.WriteLine("TicTacToe Game");
            Console.WriteLine("");
            Console.WriteLine("-----");
            Console.WriteLine($" {name}, {symbol} won!");
            Console.WriteLine("-----");
            Console.WriteLine("");
        }

        private static bool ContinuePlaying()
        {
            
            bool inputIsValid = true;
            do
            {
                Console.WriteLine("");
                Console.WriteLine("Play another? (y/n) ");
                char continuePlayingChar = Console.ReadKey().KeyChar;

                if (continuePlayingChar == 'y' || continuePlayingChar == 'Y') return true;
                else if (continuePlayingChar == 'n' || continuePlayingChar == 'N') return false;
                else
                {
                    inputIsValid = false;
                    Console.WriteLine("");
                    Console.WriteLine("Invalid input! Try again.");
                }
            } while (!inputIsValid);

            return false;    
        }

        public static char PlayerTurn (byte playerNumberTurn, char symbol)
        {
            Console.WriteLine($"Player {playerNumberTurn}");
            Console.WriteLine(" ");
            Console.WriteLine($"Choose an empty spot (1-9) for {symbol}: ");
            char positionString = Console.ReadKey().KeyChar;
            

            return positionString;

        }


        public static void SetField(char[,] playField)
        {
            Console.Clear();
            Console.WriteLine("TicTacToe Game");
            Console.WriteLine("");
            Console.WriteLine("     |     |      ");
            Console.WriteLine("  {0}  |  {1}  |  {2}", playField[0, 0], playField[0, 1], playField[0, 2]);
            Console.WriteLine("_____|_____|_____");
            Console.WriteLine("     |     |     ");
            Console.WriteLine("  {0}  |  {1}  |  {2}", playField[1, 0], playField[1, 1], playField[1, 2]);
            Console.WriteLine("_____|_____|_____");
            Console.WriteLine("     |     |     ");
            Console.WriteLine("  {0}  |  {1}  |  {2}", playField[2, 0], playField[2, 1], playField[2, 2]);
            Console.WriteLine("     |     |     ");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
        }
    }
}