namespace DesignPatterns.Visitor
{
    public abstract class NaiveEmployee
    {
        public readonly string Id;
        public readonly string Name;

        protected NaiveEmployee(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public abstract string reportHoursAndPay();
    }
    public class NaiveHourlyEmployee : NaiveEmployee
    {
        public readonly double Hours;
        public readonly double PerHour;
        #region Overrides of Employee

        public NaiveHourlyEmployee(string id, string name, double hours, double perHour) : base(id, name)
        {
            Hours = hours;
            PerHour = perHour;
        }

        public override string reportHoursAndPay()
        {
            return $"{Id,-10} | {Name,-10} | {Hours,-10} | {Hours * PerHour,-10}";
        }
        #endregion
    }

    public class NaiveSalariedEmployee : NaiveEmployee
    {
        public readonly double Hours;
        public readonly double Salary;
        #region Overrides of Employee

        public NaiveSalariedEmployee(string id, string name, double hours, double salary) : base(id, name)
        {
            Hours = hours;
            Salary = salary;
        }

        public override string reportHoursAndPay()
        {
            return $"{Id,-10} | {Name,-10} | {Hours,-10} | {Hours * Salary,-10}";
        }
        #endregion
    }

}