namespace Assignment3_HashDictionary
{
    internal class GeoLocation : IEquatable<GeoLocation>
    {
        public float Longitude { get; }
        public float Latitude { get; }
        public GeoLocation(float longitude, float latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }

        public bool Equals(GeoLocation? other)
        {
            if (other == null) return false;
            return Math.Abs(Longitude - other.Longitude) < 1 && Math.Abs(Latitude - other.Latitude) < 1;
        }
        
        // Make sure that same pair of longitude and latitude get the same hashCode
        public override int GetHashCode()
        {
            return Tuple.Create(Longitude, Latitude).GetHashCode();
        }
    }
}
