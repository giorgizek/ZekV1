namespace Zek.Extensions
{
    public static class DecimalExtensions
    {
        public static int ToFractionalUnit32(this decimal value)
        {
            return (int)(value * 100M);
        }
        public static long ToFractionalUnit64(this decimal value)
        {
            return (long)(value * 100M);
        }
    }
}
