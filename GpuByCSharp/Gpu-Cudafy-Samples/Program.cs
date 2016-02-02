using System;
using Cudafy;
using Cudafy.Host;
using Cudafy.Maths.BLAS;
using Cudafy.Maths.BLAS.Types;
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

//            BlasSample(deviceId);
        }

        //
        // http://stackoverflow.com/questions/18628447/cudafy-throws-an-exception-while-testing
        //
        private static void BlasSample(int deviceId)
        {
            GPGPU gpu = CudafyHost.GetDevice(CudafyModes.Target, deviceId);
            CudafyModes.DeviceId = deviceId;
            eArchitecture arch = gpu.GetArchitecture();
            CudafyModule km = CudafyTranslator.Cudafy(arch);
            gpu.LoadModule(km);
            
            GPGPUBLAS blas = GPGPUBLAS.Create(gpu);

            const int N = 100;
            float[] a = new float[N];
            float[] b = new float[N];
            float[] c = new float[N];
            float alpha = -1;
            float beta = 0;

            float[] device_a = gpu.CopyToDevice(a);
            float[] device_b = gpu.CopyToDevice(b);
            float[] device_c = gpu.CopyToDevice(c);

            int m = 10;
            int n = 10;
            int k = 10;
            cublasOperation Op = cublasOperation.N;            
            blas.GEMM(m, k, n, alpha, device_a, device_b, beta, device_c, Op);

            throw new NotImplementedException();
        }

        public static cublasOperation Op { get; set; }
    }
}
