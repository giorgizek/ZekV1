namespace Zek.Core
{
    /// <summary>
    /// ორობითი ოპერაციების კლასი.
    /// </summary>
    public class BitwiseHelper
    {
        public static bool HasFlag(int flags, int flagToCheck)
        {
            return (flags & flagToCheck) == flagToCheck;
        }

        public static int AddFlag(int flags, int flagToAdd)
        {
            return AddFlags(flags, flagToAdd);
        }
        public static int AddFlags(int flags, params int[] flagsToAdd)
        {
            if (flagsToAdd == null) return flags;
            for (var i = 0; i < flagsToAdd.Length; i++)
            {
                flags |= flagsToAdd[i];
            }

            return flags;
        }

        public static int DeleteFlag(int flags, int flagToDelete)
        {
            return DeleteFlags(flags, flagToDelete);
        }
        public static int DeleteFlags(int flags, params int[] flagsToDelete)
        {
            if (flagsToDelete == null) return flags;
            for (var i = 0; i < flagsToDelete.Length; i++)
            {
                flags &= ~flagsToDelete[i];
            }

            return flags;
        }

        public static int ToggleFlag(int flags, int flagToToggle)
        {
            flags ^= flagToToggle;
            return flags;
        }


        public static int MinFlag(int value)
        {
            var result = 1;
            while ((result & value) == 0 && result != 0)
                result <<= 1;
            return result;
        }

        public static int MaxFlag(int value)
        {
            var result = (int.MaxValue >> 1) + 1; // because C2
            while ((result & value) == 0 && result != 0)
                result >>= 1;
            return result;
        }

        public static long MinFlag(long value)
        {
            var result = 1;
            while ((result & value) == 0 && result != 0)
                result <<= 1;
            return result;
        }

        public static long MaxFlag(long value)
        {
            var result = (int.MaxValue >> 1) + 1; // because C2
            while ((result & value) == 0 && result != 0)
                result >>= 1;
            return result;
        }
    }
}
