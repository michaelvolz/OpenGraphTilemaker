using System;

namespace Common.Extensions
{
    public static class MathExtensions
    {
        public static double CeilingBy(this int dividend, int divisor) => Math.Ceiling((double) dividend / divisor);

        public static double FloorBy(this int dividend, int divisor) => Math.Floor((double) dividend / divisor);

        public static int ToAbs(this int arg) => Math.Abs(arg);
    }
}