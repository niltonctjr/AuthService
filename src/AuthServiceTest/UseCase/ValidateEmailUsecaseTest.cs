using AuthService.Domain.Models.Enums;
using AuthService.Extensions.InjectDependencies;
using AuthService.Providers.Mail.MailTrap;
using AuthService.Settings;
using AuthService.UseCases;
using AuthServiceTest.Mocks.Repositories;
using AuthService.Domain.Utils.Cryptography;
using AuthService.Extensions;

namespace AuthServiceTest.UseCase
{
    public class ValidateEmailUsecaseTest : BaseUsecaseTest
    {

        [Test]
        public void Run()
        {
            var iopAuthSetting = _config.GetSettingIOptions<AuthSetting>();
            var iopMailTrap = _config.GetSettingIOptions<MailTrapSetting>();
            var rep = new MockUserRepository();
            var provider = new MailTrapProvider(iopMailTrap);
            var usecase = new ValidateEmailUsecase(rep, iopAuthSetting);

            var user = new AuthService.Domain.Models.UserModel()
            {
                Email = "teste@authservice.com",
                Password = "teste123".Encryp(),
            };

            rep.Create(user);

            var token = Token.EncodeTokenMail(iopAuthSetting.Value, user);

            var actor = new Actor("EE913454-6149-08DB-283A-4D935EFFA928", "Admin");

            var resp = usecase.Execute(actor, new ValidateEmailDto() { Token = token }).Result;

            if (resp.Status != StatusDto.Success)
                Assert.Fail();
            else
                Assert.Pass();
        }
    }
}