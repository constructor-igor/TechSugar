using System;
using System.Data;
using System.IO;
using NUnit.Framework;

namespace highchartsSamples
{
    /*
     * References:
     * - https://habrahabr.ru/post/279515/
     * 
     * */
    [TestFixture]
    public class Samples
    {
        [Test]
        public async void PostSample()
        {
            throw new NotImplementedException();
//            var client = new HighchartsClient("http://export.highcharts.com/");
//
//            var options = new
//            {
//                xAxis = new
//                {
//                    categories = new[] { "Jan", "Feb", "Mar" }
//                },
//                series = new[]
//                {
//                    new { data = new[] {29.9, 71.5, 106.4} }
//                }
//            };
//
//            var res = await client.GetChartImageFromOptionsAsync(JsonConvert.SerializeObject(options));
//
//            File.WriteAllBytes("image.png", res);
        }
    }
}
