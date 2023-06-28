using Org.BouncyCastle.Utilities.Encoders;
using System.Collections;
using System.ComponentModel;

namespace AuthService.UseCases
{
    public interface IUsecase<T>
    {
        public Task<Output> Execute(Actor actor, T dto);
    }
    public abstract class BaseUsecase<T> : IUsecase<T>        
    {
        public Task<Output> Execute(Actor actor, T dto)
        {
            var fexception = (Exception ex, StatusDto status) => Task.FromResult(new Output() {
                Status = status,
                Data = new {
                    ex.Message,
                    Inner = ex.InnerException?.ToString(),
                    Stack = ex.StackTrace?.ToString(),
                    Usecase = GetType().FullName
                }
            });

            try
            {
                if (!IsValid(actor, dto))
                    throw new WarningException("Dados de entrada inválidos");

                var response = Run(actor, dto).Result;

                return Task.FromResult(new Output() {
                    Status = StatusDto.Success,
                    Data = response
                });
            }
            catch (WarningException wex) 
            {
                return fexception(wex, StatusDto.Invalid);
            }
            catch (Exception ex)
            {
                return fexception(ex, StatusDto.Error);
            }
        }
        public abstract Task<dynamic> Run(Actor actor, T dto);
        public abstract bool IsValid(Actor actor, T dto);
    }

    public class Output
    {
        public StatusDto Status { get; set; }
        public dynamic? Data { get; set; }
    }

    public enum StatusDto
    {
        Invalid = -1,
        Error = 0,
        Success = 1,
    }
}
