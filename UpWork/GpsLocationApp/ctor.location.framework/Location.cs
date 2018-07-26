using System.Collections.Generic;
using System.Device.Location;
using System.Linq;

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

        public string GetNearestLocation(double latitude, double longitude, int k)
        {
            GeoCoordinate actualCoordinate = new GeoCoordinate(latitude, longitude);

            List<LocationEntity> orderedLocations = Locations.ToList().OrderBy(entity => actualCoordinate.GetDistanceTo(entity.Coordinate)).ToList();

            List<LocationEntity> kNearest = orderedLocations.Take(k).ToList();
            List<IGrouping<string, LocationEntity>> grouped = kNearest.GroupBy(entity => entity.GeographicalName).OrderBy(g => g.Count()).ToList();
            IGrouping<string, LocationEntity> nearestList = grouped.First();
            LocationEntity nearest = nearestList.OrderBy(entity => actualCoordinate.GetDistanceTo(entity.Coordinate)).First();
            double minDistance = actualCoordinate.GetDistanceTo(nearest.Coordinate);
            var nearestGeographicalName = nearestList.Key;

            double distanceInMiles = DataConversion.ConvertMetersToMiles(minDistance);
            string direction = "";

            return $"{distanceInMiles:0.0} mi {direction} {nearestGeographicalName}";
        }
    }
}
