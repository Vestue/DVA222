namespace Assignment3_HashDictionary
{
    internal class GeoLocation
    {
        public int Longitude { get; private set; }
        public int Latitude { get; private set; }
        public GeoLocation(int longitude, int latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }
    }
}
