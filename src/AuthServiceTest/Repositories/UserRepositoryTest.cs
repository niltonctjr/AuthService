using AuthService.Domain.Models;
using AuthService.Domain.Models.Enums;
using AuthService.Providers.Cryptography;
using AuthService.Repositories;
using AuthService.Repositories.Contexts;
using AuthServiceTest.UseCase;

namespace AuthServiceTest.Repositories
{
    [Order(1)]
    public class UserRepositoryTest : BaseTest
    {
        private AuthServiceContext _ctx;
        private UserRepository _rep;
        private string _username;
        private Guid _idAdmin;

        public override void Setup()
        {
            base.Setup();
            _ctx = new AuthServiceContext(_config);
            _rep = new UserRepository(_ctx);
            _username = $"Test-{Guid.NewGuid()}@authservice.com.br";
        }
        [Test]
        [Order(2)]
        public void Get()
        {
            var result = _rep.Get(_idAdmin);
            Assert.IsNotNull(result);
        }
        [Test]
        [Order(2)]
        public void GetAll()
        {
            var result = _rep.GetAll();
            Assert.IsNotEmpty(result);
        }
        [Test]
        [Order(1)]
        public void Create() 
        {
            var encryp = new CryptographyProvider();
            var encrypPass = encryp.Encryp("123");

            var model = new UserModel(){
                Email = _username,
                Password = encrypPass,
                CreatedById = _idAdmin,
            };

            _rep.Create(model);

            var result = _rep.Get(model.Id);
            Assert.IsNotNull(result);
        }
        [Test]
        [Order(2)]
        public void Update()
        {
            var encryp = new CryptographyProvider();
            var encrypPass = encryp.Encryp("1234");

            var old = _rep.GetAll().Last();
            old.Password = encrypPass;

            _rep.Alter(old);
            var result = _rep.Get(old.Id);

            Assert.That(result?.Password, Is.EqualTo(encrypPass));
            
        }
        [Test]
        [Order(2)]
        public void Disable()
        {
            var old = _rep.GetAll().Last();
            _rep.Disable(old.Id);
            var result = _rep.Get(old.Id);

            Assert.That(result?.State, Is.EqualTo(StateGeneric.Inactive));
        }
        [Test]
        [Order(2)]
        public void Enable()
        {
            var old = _rep.GetAll().Last();
            _rep.Enable(old.Id);
            var result = _rep.Get(old.Id);

            Assert.That(result?.State, Is.EqualTo(StateGeneric.Active));
        }

        [Test]
        [Order(0)]
        public void GetByEmail()
        {
            var result = _rep.GetByEmail("admin@authservice.com");

            if(result!= null && result.Any())
                _idAdmin = result.First().Id;

            Assert.That(result, Is.Not.Empty);
        }

        [Test]
        [Order(3)]
        public void Delete()
        {
            /// Espera 3 segundos de forma a garantir que este seja o ultimo teste
            /// Esta ocorrendo problemas onde um teste iniciava junto com outro, mesmo tendo adicionado a ordenação
            Task.Delay(TimeSpan.FromSeconds(3)).Wait();

            var old = _rep.GetAll().Last();
            _rep.Delete(old.Id);

            var result = _rep.Get(old.Id);
            Assert.IsNull(result);
        }
    }
}
