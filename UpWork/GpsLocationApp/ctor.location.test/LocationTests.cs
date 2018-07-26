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
    }
}
