using System;

namespace SamplesForCodeAnalysis
{
    public class SampleWithFinalizerWithDispose
    {
        public void Dispose(bool disposing)
        {
        }

        ~SampleWithFinalizerWithDispose()
        {
            Dispose(true);
            int i = 0;
            i++;
            int k = 56;
            Dispose(false);
        }       
    }
    public class SampleWithFinalizerWithoutDispose
    {
        ~SampleWithFinalizerWithoutDispose()
        {
            //TODO implement finalizer
            Console.WriteLine("dummy finalizer");
        }
    }
}