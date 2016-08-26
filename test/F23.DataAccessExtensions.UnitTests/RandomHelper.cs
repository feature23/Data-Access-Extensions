using System;

namespace F23.DataAccessExtensions.UnitTests
{
    static class RandomHelper
    {
        private static readonly Random Random = new Random();

        public static bool NextBool() => Random.Next() % 2 == 0;
    }
}
