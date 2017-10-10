// ============================================================================
// <copyright file="Program.cs" company="The World as a Workflow">
//     Copyright (c) The World as a Workflow. All rights reserved.
// </copyright>
// <author>Alexander Nechyporenko</author>
// <date>2011-07-12</date>
// ============================================================================

namespace IsWWFUsefullSample.TestClient
{
    using System;
    using System.Activities;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using IsWWFUsefullSample.Activities;

    /// <summary>
    /// Console program class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Program starting point.
        /// </summary>
        /// <param name="args">Program parameters.</param>
        public static void Main(string[] args)
        {
            // Question answers
            const string Yes = "yes";
            const string No = "no";
            var answers = new List<string> { Yes, No };

            // Create new activity instance
            var activity = new IsWWFUsefull();
            var parameters = new Dictionary<string, object>();

            do
            {
                Console.WriteLine("Please, answer following questions to determine necessity of using WWF.");
                Console.WriteLine();

                // Read activity input parameters
                parameters["IsLongRunning"] = ReadAnswer("Is process/algorithm long running?", answers) == Yes;
                parameters["IsChangeable"] = ReadAnswer("Is process/algorithm frequently changed?", answers) == Yes;
                parameters["IsDesignerNecessary"] =
                    ReadAnswer("Do you need a visual designer for your process/algorithm?", answers) == Yes;

                // Execute activity
                var result = WorkflowInvoker.Invoke(activity, parameters);

                // Show result
                Console.WriteLine();
                if ((bool)result["Result"])
                {
                    Console.WriteLine("Use WWF!");
                }
                else
                {
                    Console.WriteLine("You don't need WWF but still can use it if you like it :).");
                }

                Console.WriteLine("---------------------------------------------------------------------");
                Console.WriteLine();
            }
            while (ReadAnswer("Do you want to proceed?", answers) == Yes);

            return; 
        }

        /// <summary>
        /// Read answer from console.
        /// </summary>
        /// <param name="question">Question text.</param>
        /// <param name="answers">A list of posible</param>
        /// <returns>Answer text.</returns>
        private static string ReadAnswer(string question, IList<string> answers)
        {
            // Prepare answers prompting string
            var answersString = new StringBuilder();
            for (var i = 0; i < answers.Count; i++)
            {
                answersString.AppendFormat(i == 0 ? "{0}" : "/{0}", answers[i]);
            }

            // Read and validate the answer
            var text = string.Empty;
            var answer = string.Empty;
            do 
            {
                if (!string.IsNullOrEmpty(text))
                {
                    Console.WriteLine(
                        string.Format("'{0}' doesn't belong to list of posible answers: '{1}'.", text, answersString));
                }

                Console.Write(string.Format("{0} ({1}): ", question, answersString));

                text = Console.ReadLine();
                answer = answers.Where(a => a == text.ToLower()).FirstOrDefault();
            }
            while (answer == null);

            return answer;
        }
    }
}
