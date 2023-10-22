using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CompleteCSharpMasterclassCE9
{
    internal class Field
    {

        public char[,] FieldArray { get; set; }
        //public char FieldPosition { get; set; }

        public Field()
        {
            FieldArray = new char[,]
            {
                { '1','2','3'},
                { '4','5','6'},
                { '7','8','9'}
            };
        }

        public void ModifyEntry(char fieldPosition, char newValue)
        {
            for (int i = 0; i < FieldArray.GetLength(0); i++) 
            {
                for (int j = 0; j < FieldArray.GetLength(1); j++)
                {
                    if (FieldArray[i, j] == fieldPosition)
                    {
                        FieldArray[i, j] = newValue;
                        return;
                    }
                        
                        
                }
            }
        }

        public bool ValidateInput(char selection)
        {
            int selectionInt = selection - '0';
            bool isAvailable = false;
            if (selectionInt >= 1 && selectionInt <= 9)
            {

                foreach (char c in FieldArray)
                {

                    if (selection == c)
                    {
                        isAvailable = true;
                        break;
                    }
                }

                if (!isAvailable)
                {
                    Console.WriteLine(""); 
                    Console.WriteLine("Input position in not empty! Try again");
                }
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine("Input out of range! Try again");
            }

            Console.WriteLine("");
            return isAvailable;
        }

        public bool CheckWin(char symbol)
        {
            int counterSymbol = 0;
            
            for (int i = 0; i < FieldArray.GetLength(0); i++)
            {
                counterSymbol = 0;
                for (int j = 0; j < FieldArray.GetLength(1); j++)
                {
                    if ((FieldArray[i, j] == symbol)) counterSymbol++;
                }
                if (counterSymbol == 3) return true;
            }

            for (int j = 0; j < FieldArray.GetLength(1); j++)
            {
                counterSymbol = 0;
                for (int i = 0; i < FieldArray.GetLength(0); i++)
                {
                    if ((FieldArray[i, j] == symbol)) counterSymbol++;
                }
                if (counterSymbol == 3) return true;
            }

            counterSymbol = 0;
            for (int i = 0, j=0;  i < FieldArray.GetLength(0); i++, j++)
            {
                if ((FieldArray[i, j] == symbol)) counterSymbol++;
            }
            if (counterSymbol == 3) return true;

            counterSymbol = 0;
            for (int i = 0, j = FieldArray.GetLength(1)-1; i < FieldArray.GetLength(0); i++, j--)
            {
                if ((FieldArray[i, j] == symbol)) counterSymbol++;
            }
            if (counterSymbol == 3) return true;

            return false;
        }


    }
}
