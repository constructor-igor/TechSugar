using System;
using Cudafy;
using Cudafy.Host;
using Cudafy.Translator;

namespace Gpu_Cudafy_Samples
{
    public class ArrayBasicIndexing
    {
        public const int N = 1 * 1024;

        public static void Execute()
        {
            int deviceId = 0;
            CudafyModes.Target = eGPUType.OpenCL;
            GPGPU gpu = CudafyHost.GetDevice(CudafyModes.Target, deviceId);
            eArchitecture arch = gpu.GetArchitecture();
            CudafyModule km = CudafyTranslator.Cudafy(arch);

            gpu.LoadModule(km);

            int[] a = new int[N];
            int[] b = new int[N];
            int[] c = new int[N];

            // allocate the memory on the GPU
            int[] dev_a = gpu.Allocate<int>(a);
            int[] dev_b = gpu.Allocate<int>(b);
            int[] dev_c = gpu.Allocate<int>(c);

            // fill the arrays 'a' and 'b' on the CPU
            for (int i = 0; i < N; i++)
            {
                a[i] = i;
                b[i] = 2 * i;
            }

            for (int l = 0; l < km.Functions.Count; l++)
            {
                string function = string.Format("add_{0}", l);
                Console.WriteLine(function);

                // copy the arrays 'a' and 'b' to the GPU
                gpu.CopyToDevice(a, dev_a);
                gpu.CopyToDevice(b, dev_b);

                gpu.Launch(128, 1, function, dev_a, dev_b, dev_c);    

                // copy the array 'c' back from the GPU to the CPU
                gpu.CopyFromDevice(dev_c, c);

                // verify that the GPU did the work we requested
                bool success = true;
                for (int i = 0; i < N; i++)
                {
                    if ((a[i] + b[i]) != c[i])
                    {
                        Console.WriteLine("{0} + {1} != {2}", a[i], b[i], c[i]);
                        success = false;
                        break;
                    }
                }
                if (success)
                    Console.WriteLine("We did it!");
            }

            // free the memory allocated on the GPU
            gpu.Free(dev_a);
            gpu.Free(dev_b);
            gpu.Free(dev_c);

            // free the memory we allocated on the CPU
            // Not necessary, this is .NET
        }

        [Cudafy]
        public static void add_0(GThread thread, int[] a, int[] b, int[] c)
        {
            int tid = thread.blockIdx.x;
            while (tid < N)
            {
                c[tid] = a[tid] + b[tid];
                tid += thread.gridDim.x;
            }
        }

        [Cudafy]
        public static void add_1(GThread thread, int[] a, int[] b, int[] c)
        {
            int tid = thread.blockIdx.x;
            while (tid < a.Length)
            {
                c[tid] = a[tid] + b[tid];
                tid += thread.gridDim.x;
            }
        }

        [Cudafy]
        public static void add_2(GThread thread, int[] a, int[] b, int[] c)
        {
            int tid = thread.blockIdx.x;
            while (tid < b.Length)
            {
                c[tid] = a[tid] + b[tid];
                tid += thread.gridDim.x;
            }
        }

        [Cudafy]
        public static void add_3(GThread thread, int[] a, int[] b, int[] c)
        {
            int tid = thread.blockIdx.x;
            while (tid < c.Length)
            {
                c[tid] = a[tid] + b[tid];
                tid += thread.gridDim.x;
            }
        }

        [Cudafy]
        public static void add_4(GThread thread, int[] a, int[] b, int[] c)
        {
            int tid = thread.blockIdx.x;
            int rank = a.Rank;
            while (tid < c.Length)
            {
                c[tid] = a[tid] + b[tid];
                tid += thread.gridDim.x;
            }
        }
    }
}