using System.Runtime.InteropServices;
using RGiesecke.DllExport;

public class Test
{
    [DllExport("add", CallingConvention = CallingConvention.Cdecl)]
    public static int TestExport(int left, int right)
    {
        return left + right;
    }
}