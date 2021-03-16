using System;
using System.Collections.Generic;
using System.Text;

namespace GestionHotelCentraliser
{
    class MyRandom
    {
        static Random _random = new Random();
        // Generates a random number within a range.
        public static int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

        public static float RandomNumberFloat(int min, int max)
        {
            return (float)_random.Next(min, max);
        }
    }
}
