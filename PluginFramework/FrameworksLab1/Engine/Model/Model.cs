using System.Collections.Generic;

namespace Engine.Model
{
    public class Model
    {
        public string Name { get; private set; }
        public string History { get; set; }
        public Algorithm Algorithm { get; private set; }
        public readonly List<ModelParameter> ModelParameters = new List<ModelParameter>();
        public readonly List<ModelMaterial> ModelMaterials = new List<ModelMaterial>();

        public Model(string name)
        {
            History = "";
            Algorithm = new Algorithm();
            Name = name;            
        }
    }
}
