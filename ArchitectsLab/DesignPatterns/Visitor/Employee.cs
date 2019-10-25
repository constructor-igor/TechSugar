using System.Text;

namespace DesignPatterns.Visitor
{
    public interface IEmployeeVisitor
    {
        void AcceptHourlyEmployee(HourlyEmployee hourlyEmployee);
        void AcceptSalariedEmployee(SalariedEmployee salariedEmployee);
    }

    public class SalaryReportGenerationVisitor : IEmployeeVisitor
    {
        public readonly StringBuilder ReportContent;
        public SalaryReportGenerationVisitor()
        {
            ReportContent = new StringBuilder();
            ReportContent.AppendLine($"{"Emp number",-10} | {"Name",-10} | {"hours",-10} | {"pay",-10} ");
        }
        #region Implementation of IEmployeeVisitor
        public void AcceptHourlyEmployee(HourlyEmployee hourlyEmployee)
        {
            ReportContent.AppendLine($"{hourlyEmployee.Id,-10} | {hourlyEmployee.Name,-10} | {hourlyEmployee.Hours,-10} | {hourlyEmployee.Hours * hourlyEmployee.PerHour,-10}");
        }
        public void AcceptSalariedEmployee(SalariedEmployee salariedEmployee)
        {
            ReportContent.AppendLine($"{salariedEmployee.Id,-10} | {salariedEmployee.Name,-10} | {salariedEmployee.Hours,-10} | {salariedEmployee.Hours * salariedEmployee.Salary,-10}");
        }
        #endregion
    }

    public abstract class Employee
    {
        public readonly string Id;
        public readonly string Name;

        protected Employee(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public abstract void Accept(IEmployeeVisitor visitor);
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
            visitor.AcceptHourlyEmployee(this);
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
            visitor.AcceptSalariedEmployee(this);
        }
        #endregion
    }
}
