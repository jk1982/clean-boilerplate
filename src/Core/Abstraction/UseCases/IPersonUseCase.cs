using Core.DTO.Requests.Person;
using Core.DTO.Responses;

namespace Core.Abstraction.UseCases
{
    public interface IPersonUseCase : IUseCaseRequestHandler<CreatePersonRequest, ServiceResponse>
    {
    }
}
