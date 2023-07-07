using AuthService.Domain.Models.Enums;
using AuthService.Domain.Utils.Cryptography;
using AuthService.Extensions.InjectDependencies;
using AuthService.Repositories.Mappers;
using AuthService.Settings;
using AuthService.UseCases;
using AuthServiceTest.Mocks.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace AuthServiceTest.UseCase
{
    public class SignInUsecaseTest: BaseUsecaseTest
    {

        [Test]
        public void Run()
        {            
            var iopAuthSetting = _config.GetSettingIOptions<AuthSetting>();
            var rep = new MockUserRepository();
            
            rep.Create(new AuthService.Domain.Models.UserModel()
            {
                Email = "teste@authservice.com",
                Password = "teste123".Encryp(),                
            });

            var usecase = new SignInUsecase(rep, iopAuthSetting);

            var actor = new Actor(Guid.NewGuid().ToString(), "unknown");

            var resp = usecase.Execute(actor, new SignInDto() { Email = "teste@authservice.com", Password = "teste123" }).Result;

            if(resp.Status!=StatusDto.Success)
                Assert.Fail();
            else 
                Assert.Pass();
        }
    }
}