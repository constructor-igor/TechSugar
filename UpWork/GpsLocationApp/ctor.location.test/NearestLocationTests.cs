using ctor.location.framework;
using NUnit.Framework;

namespace ctor.location.tests
{
    [TestFixture]
    public class NearestLocationTests
    {
        [TestCase(38.528788, -121.759743, 1, "700.9 mi  ḰENNES")]
        [TestCase(55.2494440, -127.6797220, 1, "0.0 mi  'Ksan")]
        [TestCase(55.2494440, -127.6797220, 2, "0.0 mi  'Ksan")]
        public void getNearestLocation(double latitude, double longitude, int k, string expectedNearestLocation)
        {
            string dataSetFile = TestsHelper.GetTestDataFilePath("smallDataSet.csv");
            LocationImporter importer = new LocationImporter();
            Location location = importer.ImportFromFile(dataSetFile);
            string nearestLocation = location.GetNearestLocation(latitude, longitude, k);
            Assert.That(nearestLocation, Is.EqualTo(expectedNearestLocation));
        }

        [Explicit]
        [TestCase(38.528788, -121.759743, 1, "680.9 mi  Rosedale Rock")]
        [TestCase(50.315278, - 119.156111, 1, "0.0 mi  Abbott Creek")]
        [TestCase(50.315278, - 119.156111, 5, "0.0 mi  Abbott Creek")]
        public void getNearestLocation_from_file_with_40753_entities(double latitude, double longitude, int k, string expectedNearestLocation)
        {
            string dataSetFile = TestsHelper.GetTestDataFilePath("cgn_bc_csv_eng.csv");
            LocationImporter importer = new LocationImporter();
            Location location = importer.ImportFromFile(dataSetFile);
            string nearestLocation = location.GetNearestLocation(latitude, longitude, k);
            Assert.That(nearestLocation, Is.EqualTo(expectedNearestLocation));
        }
    }
}