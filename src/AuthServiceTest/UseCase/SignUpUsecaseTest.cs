using AuthService.UseCases;
using AuthServiceTest.Mocks.Repositories;

namespace AuthServiceTest.UseCase
{
    public class SignUpUsecaseTest: BaseUsecaseTest
    {

        [Test]
        public void Run()
        {
            var rep = new MockUserRepository();
            var usecase = new SignUpUsecase(rep);

            var actor = new Actor("EE913454-6149-08DB-283A-4D935EFFA928", "Admin");

            var resp = usecase.Execute(actor, new SignUpDto() { Email = $"Test-{Guid.NewGuid()}@authservice.com", Password = "123" }).Result;

            if(resp.Status!=StatusDto.Success)
                Assert.Fail();
            else 
                Assert.Pass();
        }
    }
}