using ctor.location.framework;
using NUnit.Framework;

namespace ctor.location.tests
{
    [TestFixture]
    public class NearestLocationTests
    {
        [TestCase(38.528788, -121.759743, "700.9 mi  ḰENNES")]
        [TestCase(55.2494440, -127.6797220, "0.0 mi  'Ksan")]
        public void getNearestLocation(double latitude, double longitude, string expectedNearestLocation)
        {
            string dataSetFile = TestsHelper.GetTestDataFilePath("smallDataSet.csv");
            LocationImporter importer = new LocationImporter();
            Location location = importer.ImportFromFile(dataSetFile);
            string nearestLocation = location.GetNearestLocation(latitude, longitude);
            Assert.That(nearestLocation, Is.EqualTo(expectedNearestLocation));
        }

        [Test, Explicit]
        public void getNearestLocation_from_file_with_40753_entities()
        {
            string dataSetFile = TestsHelper.GetTestDataFilePath("cgn_bc_csv_eng.csv");
            LocationImporter importer = new LocationImporter();
            Location location = importer.ImportFromFile(dataSetFile);
            string nearestLocation = location.GetNearestLocation(latitude: 38.528788, longitude: -121.759743);
            Assert.That(nearestLocation, Is.EqualTo("680.9 mi  Rosedale Rock"));
        }
    }
}