// Guids.cs
// MUST match guids.h
using System;

namespace constructorigor.HelloWorld
{
    static class GuidList
    {
        public const string guidHelloWorldPkgString = "593b6d72-f1fc-4c9a-8054-809d0ba80a38";
        public const string guidHelloWorldCmdSetString = "abf78604-be70-41a4-8162-4ec486ec2be7";

        public static readonly Guid guidHelloWorldCmdSet = new Guid(guidHelloWorldCmdSetString);
    };
}