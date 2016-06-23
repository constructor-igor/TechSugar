using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace csharp_tips.PLINQ
{
    [TestFixture]
    public class PLinqSample
    {
        // https://msdn.microsoft.com/en-us/library/dd997425(v=vs.110).aspx
        [Test]
        public void ForEachSanitySample()
        {
            var nums = Enumerable.Range(10, 10000);
            var query = from num in nums.AsParallel()
                        where num % 10 == 0
                        select num;
            int index = 0;
            query.ForEach(e =>
            {
                index++;
                Console.WriteLine("index: {0}", index);
            });
        }
        [Test]
        public void ForEachProductionSample()
        {
            IEnumerable<string> files = Enumerable.Range(10, 10000).Select(n => string.Format("file_{0}.txt", n));
            var query = files
                .AsParallel()
                .Select(file =>
                {
                    Console.WriteLine("input {0}", file);
                    return file + file + file;
                });

            int index = 0;
            query.ForEach(e =>
            {
                index++;
                Console.WriteLine("index: {0}", index);
            });
        }
    }
}
