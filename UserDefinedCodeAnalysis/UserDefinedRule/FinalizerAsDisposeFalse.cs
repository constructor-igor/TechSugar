using System.Linq;
using Microsoft.FxCop.Sdk;

namespace UserDefinedRule
{
    internal sealed class FinalizerAsDisposeFalse : BaseFxCopRule
    {
        public FinalizerAsDisposeFalse()
            : base("FinalizerAsDisposeFalse")
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
            var method = member as Method;
            if (method == null)
            {
                // This rule only applies to fields.
                // Return a null ProblemCollection so no violations are reported for this member.
                return null;
            }

            if (method.FullName.EndsWith(".Finalize"))
            {
                bool disposeFound = method.Instructions.Any(p => p.Value is Method && (p.Value as Method).FullName.EndsWith(".Dispose(System.Boolean)"));
                if (!disposeFound)
                {
                    Resolution resolution = GetResolution(method);
                    var problem = new Problem(resolution);
                    Problems.Add(problem);
                }
                return Problems;
            }

            return Problems;
        }
    }
}