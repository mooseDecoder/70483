using System;
using System.Linq;

namespace _70483.SupplementalClasses
{
    public static class Utilities
    {
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public enum City
        {
            Chucktown = 0,
            Stuttgart = 1,
            Seattle = 2,
            Augsburg = 3,
            NewYork = 4,
            London = 5, 
            Berlin = 6,
            Silverdale = 7,
            Poulsbo = 8,
            Miami = 9

        }
        public static int RandomNumber(int min, int max)  
        {  
            Random random = new Random();  
            return random.Next(min, max);  
        }
        public static string RandomCity()
        {
            int cityVal = RandomNumber(0,9);
            
            City city = (City)cityVal;
            //Console.WriteLine("Picking nrandom number {0}, city: {1}", cityVal, city);
            return city.ToString();

        }  
    }
}