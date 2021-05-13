using Core.Abstraction;
using Core.Abstraction.Repositories;
using Core.Abstraction.UseCases;
using Core.DTO;
using Core.DTO.Requests.Person;
using Core.DTO.Responses;
using Core.Models;
using System;
using System.Threading.Tasks;

namespace Core.UseCases
{
    public class PersonUseCase : IPersonUseCase
    {
        private readonly IPersonRepository _repository;

        public PersonUseCase(IPersonRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(CreatePersonRequest message, IOutputPort<ServiceResponse> outputPort)
        {
            try
            {
                var id = await _repository.Create(new Person(message.Name));

                outputPort.Handle(id != Guid.Empty ? new ServiceResponse<Guid>(id, true) :
                    throw new Exception("Empty Guid."));

                return true;
            }
            catch (Exception ex)
            {
                outputPort.Handle(new ServiceResponse(new[] { new Error("person", ex.Message) }));
                return false;
            }
        }
    }
}
