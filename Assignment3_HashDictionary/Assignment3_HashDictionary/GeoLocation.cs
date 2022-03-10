using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3_HashDictionary
{
    internal class GeoLocation
    {
        public int _longitude { get; private set; }
        public int _latitude { get; private set; }
        public GeoLocation(int longitude, int latitude)
        {
            _longitude = longitude;
            _latitude = latitude;
        }
    }
}
