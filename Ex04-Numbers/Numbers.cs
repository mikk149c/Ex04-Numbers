using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04_Numbers
{
    public class Numbers
    {
        public static int RectangleArea(int v1, int v2)
        {
            return v1 * v2;
        }

        public static int Sum(int[] ints)
        {
            if (ints.Length < 1)
            {
                return 0;
            }
            int sum = 0;
            for (int i = 0; i < ints.Length; i++)
            {
                sum += ints[i];
            }
            return sum;
        }

        public static int Min(int[] ints)
        {
            if (ints.Length < 1)
            {
                return 0;
            }
            int min = ints[0];  //Sets the "min" to the first value in the array
            for (int i = 1; i < ints.Length; i++)   //For loob for all index 
            {
                if (min > ints[i])  //If "min" is greater than the current index in "ints", replace min with the current index
                {
                    min = ints[i];
                }
            }
            return min; //return "min"
        }

        public static int Max(int[] ints)
        {
            if (ints.Length < 1)
            {
                return 0;
            }
            int max = ints[0];
            for (int i = 1; i < ints.Length; i++)
            {
                if (max < ints[i])
                {
                    max = ints[i];
                }
            }
            return max;
        }

        public static bool Contains(int v, int[] ints)
        {
            for (int i = 0; i < ints.Length; i++)
            {
                if (ints[i] == v) return true;
            }
            return false;
        }

        public static double Average(int[] ints)
        {
            return (Double)Numbers.Sum(ints) / ints.Length;
        }
        
        public static double LineLength(int x1, int y1, int x2, int y2)
        {
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }

        public static double PolylineLength(int[] xCoords, int[] yCoords)
        {
            if (xCoords.Length != yCoords.Length || xCoords.Length < 2)
            {
                return 0;
            }
            double sum = LineLength(xCoords[0], yCoords[0], xCoords[1], yCoords[1]);
            for (int i = 1; i < xCoords.Length-1; i++)
            {
                sum += LineLength(xCoords[i], yCoords[i], xCoords[i+1], yCoords[i+1]);
            }
            return sum;
        }

        public static double PolylineLength(int[,] cords)
        {
			if (cords.GetLength(0) < 2)
			{
				return 0;
			}
            double sum = LineLength(cords[0,0], cords[0,1], cords[1,0], cords[1,1]);
            for (int i = 1; i < cords.GetLength(0) - 1; i++)
            {
                sum += LineLength(cords[i,0], cords[i,1], cords[i + 1,0], cords[i + 1,1]);
            }
            return sum;
        }

    }
}