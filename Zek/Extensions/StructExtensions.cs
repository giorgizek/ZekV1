namespace Zek.Extensions
{

    public static class StructExtensions
    {
        public static T? AsNullable<T>(this T instance) where T : struct
        {
            return instance;
        }
    }
}
