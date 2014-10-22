using Microsoft.FxCop.Sdk;

/*
 * 
 * http://blogs.msdn.com/b/codeanalysis/archive/2010/03/26/how-to-write-custom-static-code-analysis-rules-and-integrate-them-into-visual-studio-2010.aspx
 * 
 * 
 */

namespace UserDefinedRule
{
    internal abstract class BaseFxCopRule : BaseIntrospectionRule
    {
        protected BaseFxCopRule(string ruleName)
            : base(ruleName, "UserDefinedRule.RuleMetadata", typeof(BaseFxCopRule).Assembly)
        { }
    }
}