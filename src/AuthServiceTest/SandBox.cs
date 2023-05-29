

using AuthService.Models;
using AuthService.Providers.UniqueIdentify;
using System.Security.Cryptography;
using System.Text;

namespace AuthServiceTest
{
    public class Sandbox
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Run()
        {
            using MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes("senha123senha123senha123senha123"));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(sBuilder.ToString()));
            sBuilder.Clear();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x3"));
            }

            var uidProvider = new UniqueIdentifyProvider();
            var admin = new UserModel()
            {
                Email = "admin@authservice.com",
                Password = sBuilder.ToString(),
            };

            Assert.Pass();
        }
    }
}