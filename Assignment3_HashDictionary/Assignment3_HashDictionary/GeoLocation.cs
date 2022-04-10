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
            return Math.Abs(Longitude - other.Longitude) < 0.001 && Math.Abs(Latitude - other.Latitude) < 0.001;
        }

        public override bool Equals(object? obj) => Equals(obj as GeoLocation);
        
        // Make sure that same pair of longitude and latitude get the same hashCode
        public override int GetHashCode()
        {
            return Tuple.Create(Longitude, Latitude).GetHashCode();
        }
    }
}
