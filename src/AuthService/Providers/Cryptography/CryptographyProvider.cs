using System.Security.Cryptography;
using System.Text;

namespace AuthService.Providers.Cryptography
{
    public class CryptographyProvider
    {
        public string Encryp(string input)
        {
            using MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x3"));
            }

            using var sha1 = SHA1.Create();
            return Convert.ToHexString(sha1.ComputeHash(Encoding.UTF8.GetBytes(sBuilder.ToString())));
        }
    }
}
