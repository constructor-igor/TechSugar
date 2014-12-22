namespace Engine.Model
{
    public class ModelParameter
    {
        public string Name { get; private set; }
        public double Nominal { get; private set; }

        public ModelParameter(string name, double nominal)
        {
            Name = name;
            Nominal = nominal;
        }
    }
}