using AuthService.Providers.Cryptography;
using AuthService.Repositories;
using AuthService.Repositories.Contexts;
using AuthService.Repositories.Mappers;
using AuthService.UseCases;
using AuthServiceTest.UseCase;
using Humanizer;
using IdentityModel.OidcClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AuthServiceTest.Repositories
{
    public class UserRepositoryTest : BaseTest
    {
        private AuthServiceContext _ctx;
        private UserRepository _rep;
        private string _username;

        public override void Setup()
        {
            base.Setup();
            _ctx = new AuthServiceContext(_config);
            _rep = new UserRepository(_ctx);
            _username = $"Test-{Guid.NewGuid()}@authservice.com.br";
        }
        [Test]
        public void Get()
        {
            var result = _rep.Get(new Guid("EE913454-6149-08DB-283A-4D935EFFA928"));
            Assert.IsNotNull(result);
        }
        [Test]
        public void GetAll()
        {
            var result = _rep.GetAll();
            Assert.IsNotEmpty(result);
        }
        [Test]
        public void Create() 
        {
            var encryp = new CryptographyProvider();
            var encrypPass = encryp.Encryp("123");

            var model = new AuthService.Models.UserModel(){
                Email = _username,
                Password = encrypPass,
                CreatedById = new Guid("EE913454-6149-08DB-283A-4D935EFFA928"),
            };

            _rep.Create(model);

            var result = _rep.Get(model.Id);
            Assert.IsNotNull(result);
        }
        [Test]
        public void Update()
        {
            var encryp = new CryptographyProvider();
            var encrypPass = encryp.Encryp("1234");

            var old = _rep.GetAll().Last();
            old.Password = encrypPass;

            _rep.Alter(old);
            var result = _rep.Get(old.Id);

            Assert.AreEqual(encrypPass, result.Password);
            
        }
        [Test]
        public void Disable()
        {
            var old = _rep.GetAll().Last();
            _rep.Disable(old.Id);
            var result = _rep.Get(old.Id);

            Assert.AreEqual(AuthService.Models.Enums.StateGeneric.Inactive, result.State);
        }
        [Test]
        public void Enable()
        {
            var old = _rep.GetAll().Last();
            _rep.Enable(old.Id);
            var result = _rep.Get(old.Id);

            Assert.AreEqual(AuthService.Models.Enums.StateGeneric.Active, result.State);
        }

        [Test]
        public void GetByEmail()
        {
            var result = _rep.GetByEmail("admin@authservice.com");
            Assert.IsNotEmpty(result);
        }

        [Test]
        public void Delete()
        {
            var old = _rep.GetAll().Last();
            _rep.Delete(old.Id);

            var result = _rep.Get(old.Id);
            Assert.IsNull(result);
        }
    }
}
