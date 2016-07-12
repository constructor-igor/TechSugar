using System.Runtime.InteropServices;
using RGiesecke.DllExport;

public class Test
{
    [DllExport("AddInt", CallingConvention = CallingConvention.Cdecl)]
    public static int TestExportAddInt(int left, int right)
    {
        return left + right;
    }
    [DllExport("AddArray", CallingConvention = CallingConvention.Cdecl)]
    public static int[] TestExportAddArray(int left, int right)
    {
        return new int[]{left, right};
    }
    [DllExport("AddString", CallingConvention = CallingConvention.Cdecl)]
    public static string TestExportAddString(string left, string right)
    {
        return left + right;
    }
}