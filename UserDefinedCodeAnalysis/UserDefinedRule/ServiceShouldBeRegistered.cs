using System.Linq;
using Microsoft.FxCop.Sdk;

namespace UserDefinedRule
{
    internal sealed class ServiceShouldBeRegistered : BaseFxCopRule
    {
        public ServiceShouldBeRegistered()
            : base("ServiceShouldBeRegistered")
        { }

        // Only fire on non-externally visible code elements.
        public override TargetVisibilities TargetVisibility
        {
            get
            {
                return TargetVisibilities.All;
            }
        }

        public override ProblemCollection Check(TypeNode typeNode)
        {
            if (typeNode.Interfaces.Any())
            {
                InterfaceNode foundServiceInterface = typeNode.Interfaces.First(i => i.FullName.EndsWith(".IBaseService"));
                if (foundServiceInterface!=null)
                {
                    bool foundUsage = false;
                    TypeNode serviceManagerTypeNode = foundServiceInterface.DeclaringModule.Types.First(t => t.FullName.EndsWith(".ServiceManager"));
                    if (serviceManagerTypeNode != null)
                    {
                        Member member = serviceManagerTypeNode.Members.First(t => t.FullName.EndsWith(".RegisterAllServices"));
                        var method = member as Method;
                        if (method != null)
                        {
                            foundUsage = method.Instructions.Any(opcode => opcode.Value != null && opcode.Value.ToString().Contains(typeNode.FullName + "("));
                        }
                    }

                    if (!foundUsage)
                    {
                        Resolution resolution = GetResolution(typeNode.FullName);
                        var problem = new Problem(resolution);
                        Problems.Add(problem);
                    }
                }
            }
            return Problems;
        }
    }
}