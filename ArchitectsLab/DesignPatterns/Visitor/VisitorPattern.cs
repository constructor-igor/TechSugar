using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace DesignPatterns.Visitor
{
    [TestFixture]
    public class VisitorPatternTests
    {
        [Test]
        public void NaiveImplementation()
        {
            NaiveHourlyEmployee hourlyEmployee = new NaiveHourlyEmployee("1429", "John Doe", 32, 50);
            NaiveSalariedEmployee salariedEmployee = new NaiveSalariedEmployee("1532", "James Cole", 50, 3000);
            List<NaiveEmployee> employees = new List<NaiveEmployee> {hourlyEmployee, salariedEmployee};

            StringBuilder report = new StringBuilder()
                .AppendLine($"{"Emp number",-10} | {"Name",-10} | {"hours",-10} | {"pay",-10} ");

            foreach (NaiveEmployee employee in employees)
            {
                report.AppendLine(employee.reportHoursAndPay());
            }

            Console.WriteLine(report.ToString());
        }

        [Test]
        public void PatternImplementation()
        {
            HourlyEmployee hourlyEmployee = new HourlyEmployee("1429", "John Doe", 32, 50);
            SalariedEmployee salariedEmployee = new SalariedEmployee("1532", "James Cole", 50, 3000);
            List<Employee> employees = new List<Employee> { hourlyEmployee, salariedEmployee };

            SalaryReportGenerationVisitor reportGeneration = new SalaryReportGenerationVisitor();
            foreach (Employee employee in employees)
            {
                employee.Accept(reportGeneration);
            }

            Console.WriteLine(reportGeneration.ReportContent.ToString());
        }
    }
}
