using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single.Solvers;
using MathNet.Numerics.LinearAlgebra.Solvers;

namespace NumericsSamples
{
    public class MyPreconditionerFloat : IPreconditioner<float>
    {
        readonly IPreconditioner<float> m_actual = new ILU0Preconditioner();
        public MyPreconditionerFloat()
        {            
        }

        #region IPreconditioner<float>
        public void Initialize(Matrix<float> matrix)
        {
            m_actual.Initialize(matrix);
        }
        public void Approximate(Vector<float> rhs, Vector<float> lhs)
        {
            m_actual.Approximate(rhs, lhs);
        }
        #endregion
    }
}