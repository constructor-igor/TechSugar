using Microsoft.FxCop.Sdk;

namespace UserDefinedRule
{
    internal sealed class EnforceStaticLogger : BaseFxCopRule
    {
        public EnforceStaticLogger()
            : base("EnforceStaticLogger")
        { }

        // Only fire on non-externally visible code elements.
        public override TargetVisibilities TargetVisibility
        {
            get
            {
                return TargetVisibilities.All;
            }
        }

        public override ProblemCollection Check(Member member)
        {
            Field field = member as Field;
            if (field == null)
            {
                // This rule only applies to fields.
                // Return a null ProblemCollection so no violations are reported for this member.
                return null;
            }

            string actualType = field.Type.FullName;
            if (actualType == "SamplesForCodeAnalysis.ILog")
            {
                if (!field.IsStatic)
                {
                    Resolution resolution = GetResolution(field, actualType);
                    Problem problem = new Problem(resolution);
                    Problems.Add(problem);
                }
            }

            return Problems;
        }
    }
}