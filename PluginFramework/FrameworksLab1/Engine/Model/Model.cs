using System.Collections.Generic;

namespace Engine
{
    public class Model
    {
        public string Name { get; private set; }
        public readonly List<ModelParameter> ModelParameters = new List<ModelParameter>();
        public readonly List<ModelMaterial> ModelMaterials = new List<ModelMaterial>();

        public Model(string name)
        {
            Name = name;
        }
    }
}
