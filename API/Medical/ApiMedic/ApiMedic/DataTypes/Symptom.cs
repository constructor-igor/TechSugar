namespace ApiMedic.DataTypes
{
    public class Symptom
    {
        public readonly int ID;
        public readonly string Name;

        public Symptom(int id, string name)
        {
            ID = id;
            Name = name;
        }
    }
}