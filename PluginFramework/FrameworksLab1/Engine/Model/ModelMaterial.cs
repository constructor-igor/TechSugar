namespace Engine.Model
{
    public class ModelMaterial
    {
        public string Name { get; private set; }
        public double[] X { get; private set; }
        public double[] Y { get; private set; }

        public ModelMaterial(string name)
        {
            Name = name;
            X = new[]{0.0, 1.0};
            Y = new[]{0.1, 0.2};
        }
    }
}