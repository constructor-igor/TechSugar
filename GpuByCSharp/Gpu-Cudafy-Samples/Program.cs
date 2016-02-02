using System;
using Cudafy;
using Cudafy.Translator;

/*
 * 
 * original samples copied from https://cudafy.codeplex.com/
 * 
 * */

namespace Gpu_Cudafy_Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            int deviceId = 0;
            CudafyModes.Target = eGPUType.OpenCL;
            //CudafyModes.Target = eGPUType.Cuda;
            CudafyModes.DeviceId = deviceId;
            CudafyTranslator.Language = CudafyModes.Target == eGPUType.OpenCL ? eLanguage.OpenCL : eLanguage.Cuda;

            Console.WriteLine("\r\nArrayBasicIndexing");
            ArrayBasicIndexing.Execute(deviceId);

            Console.ReadLine();
        }
    }
}
