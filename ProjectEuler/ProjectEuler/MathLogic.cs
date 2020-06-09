using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectEuler
{
    class MathLogic
    {
        private static double RunTenPowerChecker()
        {
            double n = 11;
            while (1 == 1)
            {
                double x = Math.Pow(2, n);
                double y = (double)Math.Round((decimal)Math.Log10(x));
                double z = Math.Pow(10, y);
                if (Math.Abs((x - z) / z) <= 0.024)
                {
                    return n;
                }
                n++;
            }
        }

        public static long CollatzChainLength(long x, long startValue, long counter)
        {
            var instance =  SingletonCollections.Instance;
            if (x == 1)
            {
                instance.LongLongDictionary.Add(startValue, counter);
                return counter;
            }
            if(instance.LongLongDictionary.TryGetValue(x, out long y))
            {                
                return y + counter - 1;
            }
            if(x % 2 == 0)
            {
                y = counter + CollatzChainLength(x / 2, x / 2, 1);
                instance.LongLongDictionary.Add(x, y);
                return y;
            }
            y = counter + CollatzChainLength(3 * x + 1, 3 * x + 1, 1);
            instance.LongLongDictionary.Add(x, y);
            return y;
        }

        public static ulong FrontInt(string[] productGrid)
        {
            ulong x = 0;
            foreach(string s in productGrid)
            {
                x += Convert.ToUInt64(s.Substring(0, 12));
            }
            return x;
        }

        public static int LargestProductInArray(int[][] productGrid, int consecutiveVals)
        {
            int[] maxVals = new int[3];
            maxVals[0] = CheckVerticalVals(productGrid, consecutiveVals, false);
            maxVals[1] = CheckVerticalVals(productGrid, consecutiveVals, true);
            maxVals[2] = CheckDiagonalVals(productGrid, consecutiveVals);
            //maxVals[3] = CheckNegativeDiagonalVals(productGrid, consecutiveVals);
            return maxVals.Max();
        }

        private static int CheckDiagonalVals(int[][] productGrid, int consecutiveVals)
        {
            int max = 0;
            int largeCount = productGrid.Length + productGrid[0].Length + 1;
            int iCount = productGrid.Length - consecutiveVals - 1;
            int jCount = 0;
            for (int x = largeCount; x >= 0; x--)
            {
                int i = iCount;
                int j = jCount;
                while (i < productGrid.Length - consecutiveVals && j < productGrid[0].Length - consecutiveVals)
                {
                    int value = 1;
                    for (int k = 0; k < consecutiveVals; k++)
                    {
                        Console.WriteLine("Multiplying by: " + productGrid[i + k][j + k]);
                        value *= productGrid[i + k][j + k];
                    }
                    Console.WriteLine(value);
                    if (value > max)
                        max = value;
                    i++;
                    j++;
                }
                iCount = iCount - 1 >= 0 ? iCount - 1 : 0;
                jCount = iCount == 0 ? 0 : jCount++;
            }
            return max;
        }

        private static int CheckVerticalVals(int[][] productGrid, int consecutiveVals, bool Vertical)
        {
            int max = 0;
            for (int i = 0; i < productGrid.Length; i++)
            {
                for (int j = 0; j < productGrid[0].Length - consecutiveVals; j++)
                {
                    int value = 1;
                    for (int k = 0; k < consecutiveVals; k++)
                    {
                        if (Vertical)
                        {
                            value *= productGrid[j + k][i];
                        }
                        else
                        {
                            value *= productGrid[i][j + k];
                        }
                    }
                    if (value > max)
                        max = value;
                }
            }
            return max;
        }

        public long LatticePaths(int x, int y)
        {
            var map = new Dictionary<Point, int>();
            for (int j = 1; j <= y; j++) {
                for (int i = 1; i <= x; i++) {
                    if (i == 1 || j == 1) {
                        map.Add(new Point(i, j), i + j);
                        continue;
                    }
                    try
                    {
                        int newX = map[new Point(i, j - 1)] + map[new Point(i - 1, j)];
                        map.Add(new Point(i, j), newX);
                    }
                    catch
                    {
                        Console.WriteLine("({0}, {1})", i, j);
                        throw new Exception();
                    }                    
                }
            }
            return map[new Point(x, y)];
        }

        public Func<long, long> CollatzChainLengthFunction = x => CollatzChainLength(x, x, 1);

        public double LargestProduct(int x)
        {
            string v = "7316717653133062491922511967442657474235534919493496983520312774506326239578318016984801869478851843858615607891129494954595017379583319528532088055111254069874715852386305071569329096329522744304355766896648950445244523161731856403098711121722383113622298934233803081353362766142828064444866452387493035890729629049156044077239071381051585930796086670172427121883998797908792274921901699720888093776657273330010533678812202354218097512545405947522435258490771167055601360483958644670632441572215539753697817977846174064955149290862569321978468622482839722413756570560574902614079729686524145351004748216637048440319989000889524345065854122758866688116427171479924442928230863465674813919123162824586178664583591245665294765456828489128831426076900422421902267105562632111110937054421750694165896040807198403850962455444362981230987879927244284909188845801561660979191338754992005240636899125607176060588611646710940507754100225698315520005593572972571636269561882670428252483600823257530420752963450";
            double max = 0;
            for (int i = 0; i < v.Length - 12; i++)
            {
                var sub = v.Substring(i, x);
                max = max < ProductOfString(sub) ? ProductOfString(sub) : max;
            }
            return max;
        }

        public int LargestCollatzChain(int endValue, int startValue = 2) => 
            LargestValueInRangeAfterFunction(startValue, endValue, CollatzChainLengthFunction);

        private int LargestValueInRangeAfterFunction(int startValue, int endValue, Func<long, long> function)
        {
            var range = Enumerable.Range(startValue, endValue).ToList();
            long x = 0;
            long y = 0;
            int i_val = 0;
            foreach (int i in range)
            {
                Console.WriteLine("Solving " + i);
                y = function((long)i);
                if (y > x)
                {
                    x = y;
                    i_val = i;
                }
            }
            return i_val;
        }

        public static long TriangleNumber(long v)
        {

            //long y = 0;
            //for(int i = 1; i < v + 1; i++)
            //{
            //    y += i;
            //}
            //return y;

            return v == 0 ? 0 : v + TriangleNumber(v - 1);
        }

        internal long NumberOfDivisorsOfTriangleNumbers(int divisorNumber)
        {
            int x = 0;
            int y = 0;
            int i = 1;
            int z = 0;
            while (x <= divisorNumber)
            {
                y += i;
                i++;
                z = Divisors(y);
                if(z > x)
                {
                    x = z;
                    Console.WriteLine("Up to the {0}th number the max number of divisors is {1}", i, x);
                }
            }
            return y;
        }

        public int Divisors(int y)
        {
            int count = 2;
            int lastDivisor = 2;
            for (int i = 2; i < y / lastDivisor - 1; i++)
            {
                if (y % i == 0)
                {
                    count += 2;
                    lastDivisor = i;
                }
            }
            return count;
        }

        internal long SumFile(string v)
        {
            throw new NotImplementedException();
        }

        internal long SumOfPrimesBelow(int maxValue, int minValue = 2)
        {
            var numberList = Enumerable.Range(minValue, maxValue).ToList();
            long y = 0;
            for (int i = 0; numberList[0] < maxValue; i++)
            {
                y += numberList[0];
                numberList.RemoveAll(x => x % numberList[0] == 0);
                Console.WriteLine(y);
            }
            return y;
        }

        public int SeiveOfEratosthenes(int orderedPrime, int maxValue = 2000000)
        {
            var numberList = Enumerable.Range(2, maxValue).ToList();
            for(int i = 0; i < orderedPrime; i++)
            {
                numberList.RemoveAll(x => x % numberList[0] == 0);
            }
            return numberList[0];
        }

        public List<int> SeiveOfEratosthenes(int maxValue = 1000)
        {
            var numberList = Enumerable.Range(2, maxValue).ToList();
            var list = new List<int>();
            while (numberList.Count > 0)
            {
                list.Add(numberList[0]);
                numberList.RemoveAll(x => x % numberList[0] == 0);                
            }
            return list;
        }

        private double ProductOfString(string sub)
        {
            char[] x = sub.ToCharArray();
            double z = 1;
            foreach(char y in x)
            {
                z *= Char.GetNumericValue(y);
            }
            Console.WriteLine("Z = " + z);
            return z;
        }

        internal ulong Factorial(ulong v)
        {
            return v == 1 ? 1 : v * Factorial(v - 1);
        }

        public List<int> GetFactors(int i, List<int> list)
        {
            int j = 2;
            while(j < i / 2)
            {
                if( i % j == 0)
                {
                    list.Add(j);
                    break;
                }
                j++;
            }
            if(j <= i / 2)
            {
                Console.WriteLine("Getting Factors for" + i / j);
                GetFactors(i / j, list);
            }
            list.Add(j);
            return list;
        }
    }
}