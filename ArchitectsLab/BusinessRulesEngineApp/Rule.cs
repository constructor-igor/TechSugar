using System.Linq.Expressions;

namespace BusinessRulesEngineApp
{
    ///
    /// The Rule type
    /// 
    public class Rule
    {
        ///
        /// Denotes the rules predictate (e.g. Name); comparison operator(e.g. ExpressionType.GreaterThan); value (e.g. "Cole")
        /// 
        public string ComparisonPredicate { get; set; }
        public ExpressionType ComparisonOperator { get; set; }
        public string ComparisonValue { get; set; }

        /// 
        /// The rule method that 
        /// 
        public Rule(string comparisonPredicate, ExpressionType comparisonOperator, string comparisonValue)
        {
            ComparisonPredicate = comparisonPredicate;
            ComparisonOperator = comparisonOperator;
            ComparisonValue = comparisonValue;
        }
    }
}