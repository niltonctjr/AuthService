using System;
using System.Reflection.Metadata;

namespace AuthService.Providers.UniqueIdentify
{
    public class UniqueIdentifyProvider
    {
        public Guid getUID(GeoTag? geo = null)
        {
            DateTime date = DateTime.Now.ToUniversalTime();
            TimeSpan timezone = TimeZoneInfo.Local.GetUtcOffset(date);
            SByte zone = Convert.ToSByte(timezone.Hours + 12);
            Int16 lat = 0;
            Int16 lon = 0;
            Int16 alt = 0;
            byte rand = Convert.ToByte(new Random().Next(0, 255));

            if (geo != null) 
            {
                lat = Convert.ToInt16(geo.Latitude * 10);
                lon = Convert.ToInt16(geo.Longitude * 10);
                alt = geo.Altitude;
            }

            var bDate = BitConverter.GetBytes(date.ToUniversalTime().Ticks);
            var bZone = BitConverter.GetBytes(zone);
            Array.Resize(ref bZone, 1);
            var bLat = BitConverter.GetBytes(lat);
            var bLon = BitConverter.GetBytes(lon);
            var bAlt = BitConverter.GetBytes(alt);
            var bRand = BitConverter.GetBytes(rand);
            Array.Resize(ref bRand, 1);

            var bytes = bDate //8bytes
                .Concat(bZone) //1bytes
                .Concat(bLat) //2bytes
                .Concat(bLon) //2bytes
                .Concat(bAlt) //2bytes
                .Concat(bRand) //1bytes
                .ToArray();

            Array.Resize(ref bytes, 16);

            return new Guid(bytes);
        }

        public DateTime GetDate(Guid id)
        {
            var bytes = id.ToByteArray();
            Array.Resize(ref bytes, 8);
            var date = new DateTime(BitConverter.ToInt64(bytes));

            bytes = id.ToByteArray()[8..9];
            var zone = bytes.FirstOrDefault()-12;

            return date.AddHours(zone);
        }

        public GeoTag GetGeoTag(Guid id)
        {
            var bytes = id.ToByteArray()[9..11];
            var lat = BitConverter.ToInt16(bytes);

            bytes = id.ToByteArray()[11..13];
            var lon = BitConverter.ToInt16(bytes);

            bytes = id.ToByteArray()[13..15];
            var altitude = BitConverter.ToInt16(bytes);

            return new GeoTag(lat, lon, altitude);
        }
    }
}
