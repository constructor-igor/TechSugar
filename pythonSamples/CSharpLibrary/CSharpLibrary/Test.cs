using System;
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
        return new[]{left, right};
    }
    [DllExport("AddString", CallingConvention = CallingConvention.Winapi)]
    [return: MarshalAs(UnmanagedType.LPWStr)]
    public static string TestExportAddString([MarshalAs(UnmanagedType.LPWStr)] string left, [MarshalAs(UnmanagedType.LPWStr)] string right)
    {
        return left + right;
    }

    [DllExport("GetGreeting", CallingConvention = CallingConvention.Winapi)]
    [return: MarshalAs(UnmanagedType.LPWStr)]
    public static String GetGreeting()
    {
        return "Happy Managed Coding!";
    }
}