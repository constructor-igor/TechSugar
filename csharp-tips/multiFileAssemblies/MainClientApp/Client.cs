using System;
using myStringer; //The namespace created in Stringer.netmodule.

class MainClientApp
{
    // Static method Main is the entry point method.
    public static void Main()
    {
        Stringer myStringInstance = new Stringer();
        Console.WriteLine("Client code executes");
        myStringInstance.StringerMethod();
    }
}
