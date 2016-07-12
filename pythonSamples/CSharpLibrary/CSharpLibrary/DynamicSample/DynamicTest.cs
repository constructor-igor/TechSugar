using System;

/*
 * References:
 * 
 * https://www.simple-talk.com/dotnet/.net-framework/dynamic-language-integration-in-a-c-world/
 * 
 * */

namespace DynamicCS
{
    public class Calculator
    {
        public double add(double argA, double argB)
        {
            return argA + argB;
        }
        public double sub(double argA, double argB)
        {
            return argA - argB;
        }
    }
}