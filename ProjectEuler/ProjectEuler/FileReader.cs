using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler
{
    static class FileReader
    {
        internal static int[][] ArrayFromTextFile(string filePath)
        {
            List<int[]> masterList = new List<int[]>();
            string[] lines = System.IO.File.ReadAllLines(filePath);
            foreach(string line in lines){
                string[] subList = line.Split(' ');
                int[] subListInt = subList.Select(int.Parse).ToArray();
                masterList.Add(subListInt);
            }
            return masterList.ToArray();
        }

        internal static string[] TextFromTextFile(string filePath)
        {
            return System.IO.File.ReadAllLines(filePath);
        }
    }
}