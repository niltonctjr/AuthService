using System;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;

namespace AuthService.Providers.UniqueIdentify
{
    public class UniqueIdentifyProvider
    {
        public Guid getUID()
        {
            DateTime date = DateTime.Now.ToUniversalTime();
            byte[] mac = NetworkInterface.GetAllNetworkInterfaces()
                .Where(i =>
                    i.OperationalStatus == OperationalStatus.Up
                    && (!i.Description.Contains("Virtual") && !i.Description.Contains("Pseudo"))
                    && i.GetPhysicalAddress().ToString() != "")
                .Select(i => i.GetPhysicalAddress().GetAddressBytes())
                .First();
            ushort rand = Convert.ToUInt16(new Random().Next(0, 65535));

            var bDate = BitConverter.GetBytes(date.Ticks);                        
            var bRand = BitConverter.GetBytes(rand);            

            var bytes = bDate //8bytes                
                .Concat(mac) //6bytes
                .Concat(bRand) //2bytes
                .ToArray();

            Array.Resize(ref bytes, 16);

            return new Guid(bytes);
        }

        public DateTime GetDate(Guid id)
        {
            var bytes = id.ToByteArray();
            Array.Resize(ref bytes, 8);
            var date = new DateTime(BitConverter.ToInt64(bytes));
            var zone = TimeZoneInfo.Local.GetUtcOffset(DateTime.Now).Hours;

            return date.AddHours(zone);
        }

        public string GetMac(Guid id)
        {
            var bytes = id.ToByteArray()[8..14];
            var hex = Convert.ToHexString(bytes.AsSpan());
            var regex = "(.{2})(.{2})(.{2})(.{2})(.{2})(.{2})";
            var replace = "$1:$2:$3:$4:$5:$6";
            return Regex.Replace(hex, regex, replace);
        }
    }
}
