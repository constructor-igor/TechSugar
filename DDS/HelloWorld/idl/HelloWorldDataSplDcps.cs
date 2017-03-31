using DDS;
using DDS.OpenSplice.CustomMarshalers;
using System;
using System.Runtime.InteropServices;

namespace HelloWorldData
{
    #region __Msg
    [StructLayout(LayoutKind.Sequential)]
    public sealed class __Msg
    {
        public int userID;
        public IntPtr message;
        public IntPtr source;
    }
    #endregion

    #region MsgMarshaler
    public sealed class MsgMarshaler : DDS.OpenSplice.CustomMarshalers.DatabaseMarshaler
    {
        public static readonly string fullyScopedName = "HelloWorldData::Msg";
        private static readonly Type type = typeof(__Msg);
        public static readonly int Size = Marshal.SizeOf(type);

        private static readonly int offset_userID = (int)Marshal.OffsetOf(type, "userID");
        private static readonly int offset_message = (int)Marshal.OffsetOf(type, "message");
        private static readonly int offset_source = (int)Marshal.OffsetOf(type, "source");


        public override void InitEmbeddedMarshalers(IDomainParticipant participant)
        {
        }

        public override object[] SampleReaderAlloc(int length)
        {
            return new Msg[length];
        }

        public override bool CopyIn(System.IntPtr basePtr, System.IntPtr from, System.IntPtr to)
        {
            GCHandle tmpGCHandle = GCHandle.FromIntPtr(from);
            object fromData = tmpGCHandle.Target;
            return CopyIn(basePtr, fromData, to, 0);
        }

        public override bool CopyIn(System.IntPtr basePtr, object untypedFrom, System.IntPtr to, int offset)
        {
            Msg from = untypedFrom as Msg;
            if (from == null) return false;
            Write(to, offset + offset_userID, from.userID);
            if (from.message == null) return false;
            // Unbounded string: bounds check not required...
            Write(basePtr, to, offset + offset_message, ref from.message);
            if (from.source == null) return false;
            // Unbounded string: bounds check not required...
            Write(basePtr, to, offset + offset_source, ref from.source);
            return true;
        }

        public override void CopyOut(System.IntPtr from, System.IntPtr to)
        {
            GCHandle tmpGCHandleTo = GCHandle.FromIntPtr(to);
            object toObj = tmpGCHandleTo.Target;
            CopyOut(from, ref toObj, 0);
            if (toObj != tmpGCHandleTo.Target) tmpGCHandleTo.Target = toObj;
        }

        public override void CopyOut(System.IntPtr from, ref object to, int offset)
        {
            Msg dataTo = to as Msg;
            if (dataTo == null) {
                dataTo = new Msg();
                to = dataTo;
            }
            dataTo.userID = ReadInt32(from, offset + offset_userID);
            dataTo.message = ReadString(from, offset + offset_message);
            dataTo.source = ReadString(from, offset + offset_source);
        }

    }
    #endregion

}

