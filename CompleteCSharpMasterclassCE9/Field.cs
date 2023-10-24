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

        public int[,] FieldArray { get; set; }

        public Field()
        {
            FieldArray = new int[Program.FieldNumbersInARow, Program.FieldNumbersInARow];
        }

        public bool ValidateInput(int positionColumn, int positionRow)
        {
            bool isAvailable = false;
            if (FieldArray[positionColumn, positionRow] == 0) isAvailable = true;
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

        public void TransformPosition(int selection, out int positionColumn, out int positionRow)
        {
            int x = 0;

            if((selection % Program.FieldNumbersInARow) != 0)
            {
                positionColumn = selection / (int)Program.FieldNumbersInARow;
                positionRow = (selection - (positionColumn * (int)Program.FieldNumbersInARow)) - 1;
            }
            else
            {
                positionColumn = (selection / (int)Program.FieldNumbersInARow)-1;
                positionRow = (int)Program.FieldNumbersInARow - 1;
            }
            
        }

        internal void ModifyEntry(int positionColumn, int positionRow, int symbol)
        {
            FieldArray[positionColumn,positionRow] = symbol;
        }

        internal bool CheckWinner(int positionColumn, int positionRow, int symbol)
        {

            if (HorizontalCheck(positionColumn, symbol)) return true;

            if (VerticalCheck(positionRow, symbol)) return true;

            if (positionColumn == Program.FieldNumbersInARow - positionRow - 1)
                if (DiagonalCheckSlash(symbol)) return true;

            if (positionColumn == positionRow)
                if (DiagonalCheckBackslash(symbol)) return true;

            return false;
        }

        private bool DiagonalCheckBackslash(int symbol)
        {
            int symbolCount = 0;
            for (int i = 0; i < Program.FieldNumbersInARow; i++)
            {
                if (FieldArray[i, i] != symbol) break;
                else symbolCount++;
            }
            if (symbolCount == Program.FieldNumbersInARow) return true;

            return false;
        }

        private bool DiagonalCheckSlash(int symbol)
        {
            int symbolCount = 0;
            int j = 0;
            for (int i = 0; i < Program.FieldNumbersInARow; i++)
            {
                j = Program.FieldNumbersInARow - i - 1;
                if (FieldArray[i, j] != symbol) break;
                else symbolCount++;
            }
            if (symbolCount == Program.FieldNumbersInARow) return true;

            return false;
        }

        private bool VerticalCheck(int positionRow, int symbol)
        {
            int symbolCount = 0;
            for (int i = 0; i < Program.FieldNumbersInARow; i++)
            {
                if (FieldArray[i, positionRow] != symbol) break;
                else symbolCount++;
            }
            if (symbolCount == Program.FieldNumbersInARow) return true;

            return false;
        }

        private bool HorizontalCheck(int positionColumn, int symbol)
        {
            int symbolCount = 0;
            for (int i = 0; i < Program.FieldNumbersInARow; i++)
            {
                if (FieldArray[positionColumn, i] != symbol) break;
                else symbolCount++;
            }
            if (symbolCount == Program.FieldNumbersInARow) return true;

            return false;
        }
    }
}
