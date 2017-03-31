using DDS;
using DDS.OpenSplice;

namespace HelloWorldData
{
    #region IMsgDataReader
    public interface IMsgDataReader : DDS.IDataReader
    {
        ReturnCode Read(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos);

        ReturnCode Read(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples);

        ReturnCode Read(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode Read(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode Take(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos);

        ReturnCode Take(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples);

        ReturnCode Take(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode Take(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode ReadWithCondition(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition);

        ReturnCode ReadWithCondition(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            IReadCondition readCondition);

        ReturnCode TakeWithCondition(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition);

        ReturnCode TakeWithCondition(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            IReadCondition readCondition);

        ReturnCode ReadNextSample(
            Msg dataValue,
            SampleInfo sampleInfo);

        ReturnCode TakeNextSample(
            Msg dataValue,
            SampleInfo sampleInfo);

        ReturnCode ReadInstance(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode ReadInstance(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode ReadInstance(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode TakeInstance(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode TakeInstance(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode TakeInstance(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode ReadNextInstance(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode ReadNextInstance(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode ReadNextInstance(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode TakeNextInstance(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode TakeNextInstance(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode TakeNextInstance(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode ReadNextInstanceWithCondition(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode ReadNextInstanceWithCondition(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode TakeNextInstanceWithCondition(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode TakeNextInstanceWithCondition(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode ReturnLoan(
            ref Msg[] dataValues,
            ref SampleInfo[] sampleInfos);

        ReturnCode GetKeyValue(
            ref Msg key,
            InstanceHandle handle);

        InstanceHandle LookupInstance(
            Msg instance);
    }
    #endregion

    #region IMsgDataWriter
    public interface IMsgDataWriter : DDS.IDataWriter
    {
        InstanceHandle RegisterInstance(
            Msg instanceData);

        InstanceHandle RegisterInstanceWithTimestamp(
            Msg instanceData,
            Time sourceTimestamp);

        ReturnCode UnregisterInstance(
            Msg instanceData,
            InstanceHandle instanceHandle);

        ReturnCode UnregisterInstanceWithTimestamp(
            Msg instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode Write(
            Msg instanceData);

        ReturnCode Write(
            Msg instanceData,
            InstanceHandle instanceHandle);

        ReturnCode WriteWithTimestamp(
            Msg instanceData,
            Time sourceTimestamp);

        ReturnCode WriteWithTimestamp(
            Msg instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode Dispose(
            Msg instanceData,
            InstanceHandle instanceHandle);

        ReturnCode DisposeWithTimestamp(
            Msg instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode WriteDispose(
            Msg instanceData);

        ReturnCode WriteDispose(
            Msg instanceData,
            InstanceHandle instanceHandle);

        ReturnCode WriteDisposeWithTimestamp(
            Msg instanceData,
            Time sourceTimestamp);

        ReturnCode WriteDisposeWithTimestamp(
            Msg instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode GetKeyValue(
            ref Msg key,
            InstanceHandle instanceHandle);

        InstanceHandle LookupInstance(
            Msg instanceData);
    }
    #endregion

}

