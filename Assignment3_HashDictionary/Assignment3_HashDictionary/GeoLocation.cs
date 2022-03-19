namespace Assignment3_HashDictionary
{
    internal class GeoLocation : IEquatable<GeoLocation>
    {
        public float Longitude { get; private set; }
        public float Latitude { get; private set; }
        public GeoLocation(float longitude, float latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }

        public bool Equals(GeoLocation? other)
        {
            if (other == null) return false;
            if (Longitude == other.Longitude && Latitude == other.Latitude) return true;
            return false;
        }
    }
}
