using System;
using MicroServiceSamples.DataTypes;

namespace MicroServiceSamples.Infra
{
    public class Helper
    {
        public static IModel LoadModel(string modelId)
        {
            Console.WriteLine("LoadModel {0}", modelId);
            return new Model(modelId);
        }
    }
}