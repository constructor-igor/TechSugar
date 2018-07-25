using ctor.location.framework;
using NUnit.Framework;

namespace ctor.location.tests
{
    [TestFixture]
    public class LocationTests
    {
        [Test]
        public void Location_Create_Passed()
        {
            Location location = new Location();
            Assert.That(location.Locations.Length, Is.EqualTo(0));
        }

        [Test]
        public void LoadLocationsFromFile_Length_10()
        {
            string dataSetFile = TestsHelper.GetTestDataFilePath("smallDataSet.csv");
            LocationImporter importer = new LocationImporter();
            Location location = importer.ImportFromFile(dataSetFile);
            Assert.That(location.Locations.Length, Is.EqualTo(10));
        }

        [Test]
        public void getNearestLocation()
        {
            string dataSetFile = TestsHelper.GetTestDataFilePath("smallDataSet.csv");
            LocationImporter importer = new LocationImporter();
            Location location = importer.ImportFromFile(dataSetFile);
            string nearestLocation = location.GetNearestLocation(latitude: 38.528788, longitude: -121.759743);
            Assert.That(nearestLocation, Is.EqualTo(""));
        }

        [Test, Explicit]
        public void getNearestLocation_from_file_with_40753_entities()
        {
            string dataSetFile = TestsHelper.GetTestDataFilePath("cgn_bc_csv_eng.csv");
            LocationImporter importer = new LocationImporter();
            Location location = importer.ImportFromFile(dataSetFile);
            string nearestLocation = location.GetNearestLocation(latitude: 38.528788, longitude: -121.759743);
            Assert.That(nearestLocation, Is.EqualTo(""));
        }
    }
}
