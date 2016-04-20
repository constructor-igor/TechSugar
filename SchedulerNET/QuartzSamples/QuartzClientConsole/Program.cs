using QuartzClientConsole.StateSample;
using QuartzClientConsole.UserDefinedParametersSample;

namespace QuartzClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //new HelloWorldSample().StartSample();
            //new UserDefinedParametersExecuter().StartSample();
            new StateExecuter().StartSample();
        }        
    }
}
