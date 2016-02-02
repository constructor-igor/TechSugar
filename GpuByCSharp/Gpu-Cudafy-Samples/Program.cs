using System;
using Cudafy;
using Cudafy.Host;
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

//            foreach (GPGPUProperties prop in CudafyHost.GetDeviceProperties(eGPUType.Cuda, true))
//            {
//                Console.WriteLine("name: {0}", prop.Name);
//            }

            Console.WriteLine("\r\nArrayBasicIndexing");
            ArrayBasicIndexing.Execute(deviceId);
        }
    }
}
