using QuartzClientConsole.StateSample;
using QuartzClientConsole.UserDefinedCalendarSample;
using QuartzClientConsole.UserDefinedParametersSample;

namespace QuartzClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //new HelloWorldSample().StartSample();
            //new UserDefinedParametersExecuter().StartSample();
            //new StateExecuter().StartSample();
            new UserDefinedCalendarExecuter().StartSample();
        }        
    }
}
