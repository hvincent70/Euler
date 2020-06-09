using System;

namespace ProjectEuler
{
    internal class MatrixLogic
    {

        internal object SpiralDiagonals(int x)
        {
            int squareCounter = 1;
            int addTo = 2;
            int cornerValue = 3;
            for(int i = 3; i <= x; i += 2)
            {
                for(int j = 0; j < 4; j++)
                {
                    squareCounter += cornerValue;
                    Console.WriteLine(cornerValue);
                    cornerValue += addTo;
                }
                addTo += 2;
            }
            return squareCounter;
        }
    }
}