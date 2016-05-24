using System;

namespace DynamicProxy
{
	/// <summary>
	/// </summary>
	public class TestBed
	{
		/// <summary>
		/// </summary>
		[STAThread]
		static void Main( string[] args ) {
            ITest test = (ITest)SecurityProxy.NewInstance( new TestImpl() );
            test.TestFunctionOne();
            test.TestFunctionTwo( new Object(), new Object() );
		}
	}

    public interface ITest {
        void TestFunctionOne();
        Object TestFunctionTwo( Object a, Object b );
    }

    public class TestImpl : ITest {
        public void TestFunctionOne() {
            Console.WriteLine( "In TestImpl.TestFunctionOne()" );
        }

        public Object TestFunctionTwo( Object a, Object b ) {
            Console.WriteLine( "In TestImpl.TestFunctionTwo( Object a, Object b )" );
            return null;
        }
    }
}
