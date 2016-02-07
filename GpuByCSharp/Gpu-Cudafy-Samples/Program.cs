using System;
using Cudafy;
using Cudafy.Host;
using Cudafy.Maths.BLAS;
using Cudafy.Maths.BLAS.Types;
using Cudafy.Translator;

/*
 * 
 * original samples copied from https://cudafy.codeplex.com/
 * References:
 * - http://stackoverflow.com/questions/35197096/cudafy-blas-samples-cannot-be-executed-with
 * - http://stackoverflow.com/questions/18628447/cudafy-throws-an-exception-while-testing * */

namespace Gpu_Cudafy_Samples
{
    class Program
    {
        static void Main(string[] args)
        {
//            MyFirstBlasEmulatorTest();
            
//            CudafyModes.Target = eGPUType.OpenCL;
//            //CudafyModes.Target = eGPUType.Cuda;
//            CudafyModes.DeviceId = deviceId;
//            CudafyTranslator.Language = CudafyModes.Target == eGPUType.OpenCL ? eLanguage.OpenCL : eLanguage.Cuda;
//
////            foreach (GPGPUProperties prop in CudafyHost.GetDeviceProperties(eGPUType.Cuda, true))
////            {
////                Console.WriteLine("name: {0}", prop.Name);
////            }
//
////            Console.WriteLine("\r\nArrayBasicIndexing");
            ArrayBasicIndexing.Execute();
//
//            BlasSample(deviceId);
        }

        //
        // http://stackoverflow.com/questions/18628447/cudafy-throws-an-exception-while-testing
        //
        private static void BlasSample(int deviceId)
        {
            CudafyModes.Target = eGPUType.Emulator;
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

        public static void MyFirstBlasEmulatorTest()
        {
            Console.WriteLine("MyTest()");
            // Get GPU device
            CudafyModes.Target = eGPUType.Emulator;
            GPGPU gpu = CudafyHost.GetDevice(CudafyModes.Target);
            // Create GPGPUBLAS (CUBLAS Wrapper)            
            using (GPGPUBLAS blas = GPGPUBLAS.Create(gpu))
            {
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

                gpu.CopyFromDevice<float>(device_c, c);
            }
        }
    }
}

/*
  #include <stdio.h>

__global__ void cube(float * d_out, float * d_in){
    int idx = threadIdx.x;
    float value = d_in[idx];
    d_out[idx] = value*value*value;
	// Todo: Fill in this function
}

int main(int argc, char ** argv) {
	const int ARRAY_SIZE = 96;
	const int ARRAY_BYTES = ARRAY_SIZE * sizeof(float);

	// generate the input array on the host
	float h_in[ARRAY_SIZE];
	for (int i = 0; i < ARRAY_SIZE; i++) {
		h_in[i] = float(i);
	}
	float h_out[ARRAY_SIZE];

	// declare GPU memory pointers
	float * d_in;
	float * d_out;

	// allocate GPU memory
	cudaMalloc((void**) &d_in, ARRAY_BYTES);
	cudaMalloc((void**) &d_out, ARRAY_BYTES);

	// transfer the array to the GPU
	cudaMemcpy(d_in, h_in, ARRAY_BYTES, cudaMemcpyHostToDevice);

	// launch the kernel
	cube<<<1, ARRAY_SIZE>>>(d_out, d_in);

	// copy back the result array to the CPU
	cudaMemcpy(h_out, d_out, ARRAY_BYTES, cudaMemcpyDeviceToHost);

	// print out the resulting array
	for (int i =0; i < ARRAY_SIZE; i++) {
		printf("%f", h_out[i]);
		printf(((i % 4) != 3) ? "\t" : "\n");
	}

	cudaFree(d_in);
	cudaFree(d_out);

	return 0;
}
*/