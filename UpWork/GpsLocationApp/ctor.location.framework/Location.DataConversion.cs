namespace ctor.location.framework
{
    public partial class Location
    {
        public static class DataConversion
        {
            public static double ConvertMetersToMiles(double meters)
            {
                return (meters / 1609.344);
            }
        }
    }
}