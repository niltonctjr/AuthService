using System.ComponentModel;

namespace AuthService.Domain.Utils.UniqueIdentify
{
    public class GeoTag
    {        
        public GeoTag(decimal latitude = 0, decimal longitude = 0)
        {
            valid(latitude, longitude);
            Latitude = latitude;
            Longitude = longitude;
        }

        public GeoTag(decimal latitude, decimal longitude, Int16 altitude)
        {
            valid(latitude, longitude);
            Latitude = latitude;
            Longitude = longitude;
            Altitude = altitude;
        }

        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public Int16 Altitude { get; set; }

        private void valid(decimal latitude, decimal longitude)
        {
            if (latitude > 90 && latitude < -90)
                throw new WarningException("Latitude value out of range");

            if (longitude > 180 && longitude < -180)
                throw new WarningException("Longitude value out of range");
        }
    }
}