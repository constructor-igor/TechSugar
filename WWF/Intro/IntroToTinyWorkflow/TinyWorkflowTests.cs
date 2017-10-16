using System.Collections.Generic;
using NUnit.Framework;
using TinyWorkflow;

/*
 * References:
 * - http://www.alphablog.org/2013/01/12/a-little-workflow-engine/
 * 
 * */

namespace IntroToTinyWorkflow
{
    public class Model
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class ProblemContext
    {
        private readonly Dictionary<string, string> m_userInput;
        public Model Model { get; set; }

        public string GetUserInput(string key)
        {
            return m_userInput[key];
        }

        public ProblemContext()
        {
            Model = new Model();
            m_userInput = new Dictionary<string, string> {{"firstName", "Joe"}, { "lastName", "Smith" } };
        }
    }
    [TestFixture]
    public class TinyWorkflowTests
    {
        [Test]
        public void Test()
        {
            ProblemContext problemContext = new ProblemContext();
            IWorkflow<ProblemContext> workflow = new Workflow<ProblemContext>()
                .Do(EnterFirstNameAction)
                .Do(EnterLastNameAction);
            workflow.Start(problemContext);

            Assert.That(problemContext.Model.FirstName, Is.EqualTo("Joe"));
            Assert.That(problemContext.Model.LastName, Is.EqualTo("Smith"));
        }

        private static void EnterFirstNameAction(ProblemContext obj)
        {
            string name = obj.GetUserInput("firstName");
            obj.Model.FirstName = name;
        }
        private static void EnterLastNameAction(ProblemContext obj)
        {
            string name = obj.GetUserInput("lastName");
            obj.Model.LastName = name;
        }
    }
}
