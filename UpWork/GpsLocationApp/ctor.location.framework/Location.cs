using System.Device.Location;

namespace ctor.location.framework
{
    public class LocationEntity
    {
        public readonly string GeographicalName;
        public readonly GeoCoordinate Coordinate;

        public LocationEntity(string geographicalName, GeoCoordinate coordinate)
        {
            GeographicalName = geographicalName;
            Coordinate = coordinate;
        }
    }
    public partial class Location
    {
        public readonly LocationEntity[] Locations;
        public Location(LocationEntity[] locations)
        {
            Locations = locations;
        }

        public Location() : this(new LocationEntity[] {})
        {

        }

        public string GetNearestLocation(double latitude, double longitude)
        {
            GeoCoordinate actualCoordinate = new GeoCoordinate(latitude, longitude);
            double minDistance = double.MaxValue;
            int minIndex = -1;

            for (int i = 0; i < Locations.Length; i++)
            {
                LocationEntity entity = Locations[i];
                double entityDistance = actualCoordinate.GetDistanceTo(entity.Coordinate);
                if (minDistance > entityDistance)
                {
                    minDistance = entityDistance;
                    minIndex = i;
                }
            }

            double distanceInMiles = DataConversion.ConvertMetersToMiles(minDistance);
            string direction = "";
            string nearestGeographicalName = Locations[minIndex].GeographicalName;

            return $"{distanceInMiles:#.#} mi {direction} {nearestGeographicalName}";
        }
    }
}
