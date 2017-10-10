using System;
using System.Activities;
using System.Activities.Statements;
using Microsoft.VisualBasic.Activities;

/*
 * https://msdn.microsoft.com/en-us/library/ee342461.aspx
 * */


namespace Intro
{
    class Program
    {
        static void Main()
        {
            Sequence_WriteLine();
            Sequence_WriteLine_Variables();

            Model model = new Model();
            Sequence sequence = new Sequence
            {
                Activities =
                {
                    new AskDataActivity {Question = "First name: ", Model = new InArgument<Model>((ctx)=>model)},
                    new AskDataActivity {Question = "Last name: "}
                }
            };
            WorkflowInvoker.Invoke(sequence);
        }

        private static void Sequence_WriteLine_Variables()
        {
            Console.WriteLine("Sequence_WriteLine + Variables");
            Sequence sequence = new Sequence
            {
                Variables =
                {
                    new Variable<string> {Default = "Hello", Name = "greeting"},
                    new Variable<string> {Default = "Bill", Name = "name"}
                },
                Activities =
                {
                    new WriteLine {Text = new VisualBasicValue<string>("greeting")},
                    new WriteLine {Text = new VisualBasicValue<string>("name + \"Gates\"")}
                }
            };
            WorkflowInvoker.Invoke(sequence);
        }

        private static void Sequence_WriteLine()
        {
            Console.WriteLine("Sequence_WriteLine");
            Sequence sequence = new Sequence
            {
                Activities =
                {
                    new WriteLine {Text = "Hello"},
                    new Sequence
                    {
                        Activities =    
                        {
                            new WriteLine {Text = "Workflow"},
                            new WriteLine {Text = "Word"}
                        }
                    }
                }
            };

            WorkflowInvoker.Invoke(sequence);
        }
    }

    [Serializable]
    public class Model
    {
        public string FirstName { get; set; }
        public string LasName { get; set; }
    }

    public enum StepStatus { prev, next, repeat }
    public class AskDataActivity : CodeActivity<StepStatus>
    {
        private RuntimeArgument outMyArgument;

        public InArgument<string> Question { get; set; }
        public InArgument Model { get; set; }

        #region CodeActivity<StepStatus>
        protected override StepStatus Execute(CodeActivityContext context)
        {
            Console.Write(Question.Get(context));
            string answer = Console.ReadLine();
            Model model = Model.Get<Model>(context);
            model.FirstName = answer;
            return StepStatus.next;
        }
        #endregion
//        protected override void CacheMetadata(CodeActivityMetadata metadata)
//        {
//            outMyArgument = new RuntimeArgument("MyUntypedArgument", MyUntypedArgument.ArgumentType, ArgumentDirection.Out);
//            metadata.Bind(MyUntypedArgument, outMyArgument);
//            metadata.AddArgument(outMyArgument);
//        }
    }
}
