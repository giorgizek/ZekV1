using System.Runtime.InteropServices;

namespace Zek.Windows
{
    internal static class NativeMethods
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool MessageBeep(int uType);
        
    }
}
