using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using DDS;
using DDS.OpenSplice;
using DDS.OpenSplice.CustomMarshalers;

namespace HelloWorldData
{
    #region MsgDataReader
    public class MsgDataReader : DDS.OpenSplice.DataReader, IMsgDataReader
    {
        public MsgDataReader(IntPtr gapiPtr)
            : base(gapiPtr) { }

        public ReturnCode Read(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos)
        {
            return Read(ref dataValues, ref sampleInfos, Length.Unlimited);
        }

        public ReturnCode Read(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples)
        {
            return Read(ref dataValues, ref sampleInfos, maxSamples, SampleStateKind.Any,
                ViewStateKind.Any, InstanceStateKind.Any);
        }

        public ReturnCode Read(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates)
        {
            return Read(ref dataValues, ref sampleInfos, Length.Unlimited, sampleStates,
                viewStates, instanceStates);
        }

        public ReturnCode Read(
                ref Msg[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            object[] objectValues = dataValues;
            ReturnCode result =
                DDS.OpenSplice.FooDataReader.Read(
                        this,
                        ref objectValues,
                        ref sampleInfos,
                        maxSamples,
                        sampleStates,
                        viewStates,
                        instanceStates);
            dataValues = (Msg[])objectValues;
            return result;
        }

        public ReturnCode Take(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos)
        {
            return Take(ref dataValues, ref sampleInfos, Length.Unlimited);
        }

        public ReturnCode Take(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples)
        {
            return Take(ref dataValues, ref sampleInfos, maxSamples, SampleStateKind.Any,
                ViewStateKind.Any, InstanceStateKind.Any);
        }

        public ReturnCode Take(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates)
        {
            return Take(ref dataValues, ref sampleInfos, Length.Unlimited, sampleStates,
                viewStates, instanceStates);
        }

        public ReturnCode Take(
                ref Msg[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            object[] objectValues = dataValues;
            ReturnCode result =
                DDS.OpenSplice.FooDataReader.Take(
                        this,
                        ref objectValues,
                        ref sampleInfos,
                        maxSamples,
                        sampleStates,
                        viewStates,
                        instanceStates);
            dataValues = (Msg[])objectValues;
            return result;
        }

        public ReturnCode ReadWithCondition(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition)
        {
            return ReadWithCondition(ref dataValues, ref sampleInfos,
                Length.Unlimited, readCondition);
        }

        public ReturnCode ReadWithCondition(
                ref Msg[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                IReadCondition readCondition)
        {
            object[] objectValues = dataValues;
            ReturnCode result =
                DDS.OpenSplice.FooDataReader.ReadWithCondition(
                        this,
                        ref objectValues,
                        ref sampleInfos,
                        maxSamples,
                        readCondition);
            dataValues = (Msg[])objectValues;
            return result;
        }

        public ReturnCode TakeWithCondition(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition)
        {
            return TakeWithCondition(ref dataValues, ref sampleInfos,
                Length.Unlimited, readCondition);
        }

        public ReturnCode TakeWithCondition(
                ref Msg[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                IReadCondition readCondition)
        {
            object[] objectValues = dataValues;
            ReturnCode result =
                DDS.OpenSplice.FooDataReader.TakeWithCondition(
                        this,
                        ref objectValues,
                        ref sampleInfos,
                        maxSamples,
                        readCondition);
            dataValues = (Msg[])objectValues;
            return result;
        }

        public ReturnCode ReadNextSample(
                Msg dataValue,
                SampleInfo sampleInfo)
        {
            object objectValues = dataValue;
            ReturnCode result =
                DDS.OpenSplice.FooDataReader.ReadNextSample(
                        this,
                        ref objectValues,
                        ref sampleInfo);
            dataValue = (Msg)objectValues;
            return result;
        }

        public ReturnCode TakeNextSample(
                Msg dataValue,
                SampleInfo sampleInfo)
        {
            object objectValues = dataValue;
            ReturnCode result =
                DDS.OpenSplice.FooDataReader.TakeNextSample(
                        this,
                        ref objectValues,
                        ref sampleInfo);
            dataValue = (Msg)objectValues;
            return result;
        }

        public ReturnCode ReadInstance(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return ReadInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode ReadInstance(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return ReadInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public ReturnCode ReadInstance(
                ref Msg[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            object[] objectValues = dataValues;
            ReturnCode result =
                DDS.OpenSplice.FooDataReader.ReadInstance(
                        this,
                        ref objectValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            dataValues = (Msg[])objectValues;
            return result;
        }

        public ReturnCode TakeInstance(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return TakeInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode TakeInstance(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return TakeInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public ReturnCode TakeInstance(
                ref Msg[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            object[] objectValues = dataValues;
            ReturnCode result =
                DDS.OpenSplice.FooDataReader.TakeInstance(
                        this,
                        ref objectValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            dataValues = (Msg[])objectValues;
            return result;
        }

        public ReturnCode ReadNextInstance(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return ReadNextInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode ReadNextInstance(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return ReadNextInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public ReturnCode ReadNextInstance(
                ref Msg[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            object[] objectValues = dataValues;
            ReturnCode result =
                DDS.OpenSplice.FooDataReader.ReadNextInstance(
                        this,
                        ref objectValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            dataValues = (Msg[])objectValues;
            return result;
        }

        public ReturnCode TakeNextInstance(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return TakeNextInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode TakeNextInstance(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return TakeNextInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public ReturnCode TakeNextInstance(
                ref Msg[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            object[] objectValues = dataValues;
            ReturnCode result =
                DDS.OpenSplice.FooDataReader.TakeNextInstance(
                        this,
                        ref objectValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            dataValues = (Msg[])objectValues;
            return result;
        }

        public ReturnCode ReadNextInstanceWithCondition(
                ref Msg[] dataValues,
                ref SampleInfo[] sampleInfos,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            return ReadNextInstanceWithCondition(
                ref dataValues,
                ref sampleInfos,
                Length.Unlimited,
                instanceHandle,
                readCondition);
        }

        public ReturnCode ReadNextInstanceWithCondition(
                ref Msg[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            object[] objectValues = dataValues;
            ReturnCode result =
                DDS.OpenSplice.FooDataReader.ReadNextInstanceWithCondition(
                        this,
                        ref objectValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        readCondition);
            dataValues = (Msg[])objectValues;
            return result;
        }

        public ReturnCode TakeNextInstanceWithCondition(
                ref Msg[] dataValues,
                ref SampleInfo[] sampleInfos,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            return TakeNextInstanceWithCondition(
                ref dataValues,
                ref sampleInfos,
                Length.Unlimited,
                instanceHandle,
                readCondition);
        }

        public ReturnCode TakeNextInstanceWithCondition(
                ref Msg[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            object[] objectValues = dataValues;
            ReturnCode result =
                DDS.OpenSplice.FooDataReader.TakeNextInstanceWithCondition(
                        this,
                        ref objectValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        readCondition);

            dataValues = (Msg[])objectValues;
            return result;
        }

        public ReturnCode ReturnLoan(
                ref Msg[] dataValues,
                ref SampleInfo[] sampleInfos)
        {
            ReturnCode result;

            if (dataValues != null && sampleInfos != null)
            {
                if (dataValues != null && sampleInfos != null)
                {
                    if (dataValues.Length == sampleInfos.Length)
                    {
                        dataValues = null;
                        sampleInfos = null;
                        result = ReturnCode.Ok;
                    }
                    else
                    {
                        result = ReturnCode.PreconditionNotMet;
                    }
                }
                else
                {
                    if ((dataValues == null) && (sampleInfos == null))
                    {
                        result = ReturnCode.Ok;
                    }
                    else
                    {
                        result = ReturnCode.PreconditionNotMet;
                    }
                }
            }
            else
            {
                result = ReturnCode.BadParameter;
            }

            return result;
        }

        public ReturnCode GetKeyValue(
                ref Msg key,
                InstanceHandle handle)
        {
            object keyObj = key;
            ReturnCode result = DDS.OpenSplice.FooDataReader.GetKeyValue(
                        this,
                        ref keyObj,
                        handle);
            if (keyObj != key) key = keyObj as Msg;
            return result;
        }

        public InstanceHandle LookupInstance(
                Msg instance)
        {
            return
                DDS.OpenSplice.FooDataReader.LookupInstance(
                        this,
                        instance);
        }

    }
    #endregion
    
    #region MsgDataWriter
    public class MsgDataWriter : DDS.OpenSplice.DataWriter, IMsgDataWriter
    {
        public MsgDataWriter(IntPtr gapiPtr)
            : base(gapiPtr) { }

        public InstanceHandle RegisterInstance(
                Msg instanceData)
        {
            return DDS.OpenSplice.FooDataWriter.RegisterInstance(
                        this,
                        instanceData);
        }

        public InstanceHandle RegisterInstanceWithTimestamp(
                Msg instanceData,
                Time sourceTimestamp)
        {
            return DDS.OpenSplice.FooDataWriter.RegisterInstanceWithTimestamp(
                        this,
                        instanceData,
                        sourceTimestamp);
        }

        public ReturnCode UnregisterInstance(
                Msg instanceData,
                InstanceHandle instanceHandle)
        {
            return DDS.OpenSplice.FooDataWriter.UnregisterInstance(
                        this,
                        instanceData,
                        instanceHandle);
        }

        public ReturnCode UnregisterInstanceWithTimestamp(
                Msg instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return DDS.OpenSplice.FooDataWriter.UnregisterInstanceWithTimestamp(
                        this,
                        instanceData,
                        instanceHandle,
                        sourceTimestamp);
        }

        public ReturnCode Write(Msg instanceData)
        {
            return Write(instanceData, InstanceHandle.Nil);
        }

        public ReturnCode Write(
                Msg instanceData,
                InstanceHandle instanceHandle)
        {
            return DDS.OpenSplice.FooDataWriter.Write(
                        this,
                        instanceData,
                        instanceHandle);
        }

        public ReturnCode WriteWithTimestamp(
                Msg instanceData,
                Time sourceTimestamp)
        {
            return WriteWithTimestamp(instanceData, InstanceHandle.Nil, sourceTimestamp);
        }

        public ReturnCode WriteWithTimestamp(
                Msg instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return DDS.OpenSplice.FooDataWriter.WriteWithTimestamp(
                        this,
                        instanceData,
                        instanceHandle,
                        sourceTimestamp);
        }

        public ReturnCode Dispose(
                Msg instanceData,
                InstanceHandle instanceHandle)
        {
            return DDS.OpenSplice.FooDataWriter.Dispose(
                        this,
                        instanceData,
                        instanceHandle);
        }

        public ReturnCode DisposeWithTimestamp(
                Msg instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return DDS.OpenSplice.FooDataWriter.DisposeWithTimestamp(
                        this,
                        instanceData,
                        instanceHandle,
                        sourceTimestamp);
        }

        public ReturnCode WriteDispose(
                Msg instanceData)
        {
            return WriteDispose(instanceData, InstanceHandle.Nil);
        }

        public ReturnCode WriteDispose(
                Msg instanceData,
                InstanceHandle instanceHandle)
        {
            return DDS.OpenSplice.FooDataWriter.WriteDispose(
                        this,
                        instanceData,
                        instanceHandle);
        }

        public ReturnCode WriteDisposeWithTimestamp(
                Msg instanceData,
                Time sourceTimestamp)
        {
            return WriteDisposeWithTimestamp(instanceData, InstanceHandle.Nil, sourceTimestamp);
        }

        public ReturnCode WriteDisposeWithTimestamp(
                Msg instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return DDS.OpenSplice.FooDataWriter.WriteDisposeWithTimestamp(
                        this,
                        instanceData,
                        instanceHandle,
                        sourceTimestamp);
        }

        public ReturnCode GetKeyValue(
                ref Msg key,
                InstanceHandle instanceHandle)
        {
            object keyObj = key;
            ReturnCode result = DDS.OpenSplice.FooDataWriter.GetKeyValue(
                                    this,
                                    ref keyObj,
                                    instanceHandle);
            if (keyObj != key) key = keyObj as Msg;
            return result;
        }

        public InstanceHandle LookupInstance(
            Msg instanceData)
        {
            InstanceHandle result = DDS.OpenSplice.FooDataWriter.LookupInstance(
                                        this,
                                        instanceData);
            return result;
        }
    }
    #endregion

    #region MsgTypeSupport
    public class MsgTypeSupport : DDS.OpenSplice.TypeSupport
    {
        private const string typeName = "HelloWorldData::Msg";
        private const string keyList = "userID";
        private readonly string[] metaDescriptor = {"<MetaData version=\"1.0.0\"><Module name=\"HelloWorldData\"><Struct name=\"Msg\"><Member name=\"userID\">",
"<Long/></Member><Member name=\"message\"><String/></Member><Member name=\"source\"><String/></Member>",
"</Struct></Module></MetaData>"};

        public MsgTypeSupport()
            : base(typeof(Msg), new MsgTypeSupportFactory())
        { }

        public override string TypeName
        {
            get
            {
                return typeName;
            }
        }

        public override string[] Description
        {
            get
            {
                return metaDescriptor;
            }
        }

        public override string KeyList
        {
            get
            {
                return keyList;
            }
        }

        public override ReturnCode RegisterType(IDomainParticipant participant, string typeName)
        {
            return RegisterType(participant, typeName, new MsgMarshaler());
        }
    }
    #endregion

    #region MsgTypeSupportFactory
    public class MsgTypeSupportFactory : DDS.OpenSplice.TypeSupportFactory
    {
        public override DDS.OpenSplice.DataWriter CreateDataWriter(IntPtr gapiPtr)
        {
            return new MsgDataWriter(gapiPtr);
        }

        public override DDS.OpenSplice.DataReader CreateDataReader(IntPtr gapiPtr)
        {
            return new MsgDataReader(gapiPtr);
        }
    }
    #endregion

}

