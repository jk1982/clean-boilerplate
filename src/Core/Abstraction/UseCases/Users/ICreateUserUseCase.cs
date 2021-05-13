using Core.DTO.Requests.Users;
using Core.DTO.Responses.Users;

namespace Core.Abstraction.UseCases.Users
{
    public interface ICreateUserUseCase : IUseCaseRequestHandler<CreateUserRequest, UserResponse>
    {
    }
}
