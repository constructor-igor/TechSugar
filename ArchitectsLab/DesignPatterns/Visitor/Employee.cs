using System.Text;

namespace DesignPatterns.Visitor
{
    public interface IEmployeeVisitor
    {
        void VisitHourlyEmployee(HourlyEmployee hourlyEmployee);
        void VisitSalariedEmployee(SalariedEmployee salariedEmployee);
    }

    public class SalaryReportGenerationVisitor : IEmployeeVisitor
    {
        public readonly StringBuilder ReportContent;
        public SalaryReportGenerationVisitor()
        {
            ReportContent = new StringBuilder();
            ReportContent.AppendLine($"{"Emp number",-10} | {"Name",-10} | {"hours",-10} | {"pay",-10} ");
        }
        #region IEmployeeVisitor
        public void VisitHourlyEmployee(HourlyEmployee hourlyEmployee)
        {
            ReportContent.AppendLine($"{hourlyEmployee.Id,-10} | {hourlyEmployee.Name,-10} | {hourlyEmployee.Hours,-10} | {hourlyEmployee.Hours * hourlyEmployee.PerHour,-10}");
        }
        public void VisitSalariedEmployee(SalariedEmployee salariedEmployee)
        {
            ReportContent.AppendLine($"{salariedEmployee.Id,-10} | {salariedEmployee.Name,-10} | {salariedEmployee.Hours,-10} | {salariedEmployee.Hours * salariedEmployee.Salary,-10}");
        }
        #endregion
    }

    public interface IEmployee
    {
        string Id { get; }
        string Name { get; }
        void Accept(IEmployeeVisitor visitor);
    }
    public abstract class Employee: IEmployee
    {
        #region IEmployee
        public string Id { get; }
        public string Name { get; }
        public abstract void Accept(IEmployeeVisitor visitor);
        #endregion

        protected Employee(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
    public class HourlyEmployee : Employee
    {
        public readonly double Hours;
        public readonly double PerHour;

        public HourlyEmployee(string id, string name, double hours, double perHour) : base(id, name)
        {
            Hours = hours;
            PerHour = perHour;
        }

        #region Employee
        public override void Accept(IEmployeeVisitor visitor)
        {
            visitor.VisitHourlyEmployee(this);
        }
        #endregion
    }
    public class SalariedEmployee : Employee
    {
        public readonly double Hours;
        public readonly double Salary;

        public SalariedEmployee(string id, string name, double hours, double salary) : base(id, name)
        {
            Hours = hours;
            Salary = salary;
        }

        #region Employee
        public override void Accept(IEmployeeVisitor visitor)
        {
            visitor.VisitSalariedEmployee(this);
        }
        #endregion
    }
}
