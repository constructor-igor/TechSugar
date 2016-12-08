using System;
using System.Dynamic;
using DynamicCS;

namespace DynamicSample
{
    public class DynamicCalc : DynamicObject
    {
        private readonly Calculator m_calculator;
        public DynamicCalc()
        {
            m_calculator = new Calculator();
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = null;
            switch (binder.Name)
            {
                case "add":
                    result = (Func<double, double, double>)((double a, double b) => m_calculator.add(a, b));
                    return true;
                case "sub":
                    result = (Func<double, double, double>)((double a, double b) => m_calculator.sub(a, b));
                    return true;
            }
            return false;
        }
    }
}
