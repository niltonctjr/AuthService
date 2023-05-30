

using AuthService.Models;
using AuthService.Providers.Cryptography;
using AuthService.Providers.UniqueIdentify;
using Newtonsoft.Json.Converters;
using System.Net.NetworkInformation;
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
            var crypto = new CryptographyProvider();
            var admin = new UserModel()
            {
                Email = "admin@authservice.com",
                Password = crypto.Encryp("senha123")
            };

            var mac = new UniqueIdentifyProvider().GetMac(admin.Id);

            Assert.Pass();
        }
    }
}