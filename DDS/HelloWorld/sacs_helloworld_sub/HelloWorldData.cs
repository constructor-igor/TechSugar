using DDS;
using System.Runtime.InteropServices;

namespace HelloWorldData
{
    #region Msg
    [StructLayout(LayoutKind.Sequential)]
    public sealed class Msg
    {
        public int userID;
        public string message = string.Empty;
        public string source = string.Empty;
    };
    #endregion

}

