using Core.Abstraction;
using Core.DTO.Responses;

namespace Core.DTO.Requests.Person
{
    public class CreatePersonRequest : IUseCaseRequest<ServiceResponse>
    {
        public CreatePersonRequest(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
