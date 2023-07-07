using AuthService.Domain.Models.Enums;
using AuthService.Extensions.InjectDependencies;
using AuthService.Providers.Mail.MailTrap;
using AuthService.Settings;
using AuthService.UseCases;
using AuthServiceTest.Mocks.Repositories;

namespace AuthServiceTest.UseCase
{
    public class SignUpUsecaseTest: BaseUsecaseTest
    {

        [Test]
        public void Run()
        {
            var iopMailTrap = _config.GetSettingIOptions<MailTrapSetting>();
            var rep = new MockUserRepository();
            var provider = new MailTrapProvider(iopMailTrap);
            var usecase = new SignUpUsecase(rep, provider);

            var actor = new Actor("EE913454-6149-08DB-283A-4D935EFFA928", "Admin");

            var resp = usecase.Execute(actor, new SignUpDto() { Email = $"Test-{Guid.NewGuid()}@authservice.com", Password = "123" }).Result;

            if (resp.Status!=StatusDto.Success)
                Assert.Fail();
            else 
                Assert.Pass();
        }
    }
}