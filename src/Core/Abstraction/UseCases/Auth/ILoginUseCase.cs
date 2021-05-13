using Core.DTO.Requests.Auth;
using Core.DTO.Responses.Auth;

namespace Core.Abstraction.UseCases.Auth
{
    public interface ILoginUseCase : IUseCaseRequestHandler<LoginRequest, LoginResponse>
    {

    }
}
