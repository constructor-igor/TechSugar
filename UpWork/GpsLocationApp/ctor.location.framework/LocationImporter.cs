using System;
using System.Device.Location;
using System.IO;
using System.Linq;

namespace ctor.location.framework
{
    public class LocationImporter
    {
        private const string LATITUDE_COLUMN_NAME = "Latitude";
        private const string LONGITUDE_COLUMN_NAME = "Longitude";
        private const string GEOGRAPHICAL_NAME_COLUMN_NAME = "Geographical Name";

        public Location ImportFromFile(string dataSetFile)
        {
            if (!File.Exists(dataSetFile))
                throw new ArgumentException();

            string[] headerItems = File.ReadLines(dataSetFile).First().Split(',');
            int latitudeIndex = -1;
            int longitudeIndex = -1;
            int geographicalNameIndex = -1;

            for (int i = 0; i < headerItems.Length; i++)
            {
                if (headerItems[i] == LATITUDE_COLUMN_NAME)
                    latitudeIndex = i;
                if (headerItems[i] == LONGITUDE_COLUMN_NAME)
                    longitudeIndex = i;
                if (headerItems[i] == GEOGRAPHICAL_NAME_COLUMN_NAME)
                    geographicalNameIndex = i;
            }
            if (latitudeIndex*longitudeIndex*geographicalNameIndex<0)
                throw new ArgumentException("Not found expected columns");

            LocationEntity[] allLocations = File
                .ReadLines(dataSetFile)
                .Skip(1)
                .Select(SplitterHelper.Splitter)
                .Select(dataItems =>
                {
                    string name = dataItems[geographicalNameIndex];
                    try
                    {
                        double latitude = Convert.ToDouble(dataItems[latitudeIndex]);
                        double longitude = Convert.ToDouble(dataItems[longitudeIndex]);
                        return new LocationEntity(name, new GeoCoordinate(latitude, longitude));
                    }
                    catch (Exception e)
                    {
                        throw new ArgumentException($"Found coordinates conversation issue in {name}", e);
                    }
                }).ToArray();
            return new Location(allLocations);
        }
    }
}