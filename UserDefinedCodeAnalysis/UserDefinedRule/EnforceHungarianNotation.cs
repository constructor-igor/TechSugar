//using System;
//using Microsoft.FxCop.Sdk;
//
//namespace UserDefinedRule
//{
//    internal sealed class EnforceHungarianNotation : BaseFxCopRule
//    {
//        public EnforceHungarianNotation()
//            : base("EnforceHungarianNotation")
//        { }
//
//        // Only fire on non-externally visible code elements.
//        public override TargetVisibilities TargetVisibility
//        {
//            get
//            {
//                return TargetVisibilities.NotExternallyVisible;
//            }
//        }
//
//        public override ProblemCollection Check(Member member)
//        {
//            Field field = member as Field;
//            if (field == null)
//            {
//                // This rule only applies to fields.
//                // Return a null ProblemCollection so no violations are reported for this member.
//                return null;
//            }
//
//            if (field.IsStatic)
//            {
//                CheckFieldName(field, s_staticFieldPrefix);
//            }
//            else
//            {
//                CheckFieldName(field, s_nonStaticFieldPrefix);
//            }
//
//            // By default the Problems collection is empty so no violations will be reported
//            // unless CheckFieldName found and added a problem.
//            return Problems;
//        }
//        private const string s_staticFieldPrefix = "s_";
//        private const string s_nonStaticFieldPrefix = "m_";
//
//        private void CheckFieldName(Field field, string expectedPrefix)
//        {
//            if (!field.Name.Name.StartsWith(expectedPrefix, StringComparison.Ordinal))
//            {
//                Resolution resolution = GetResolution(
//                  field,  // Field {0} is not in Hungarian notation.
//                  expectedPrefix  // Field name should be prefixed with {1}.
//                  );
//                Problem problem = new Problem(resolution);
//                Problems.Add(problem);
//            }
//        }
//    }
//}