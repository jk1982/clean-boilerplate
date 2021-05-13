using Core.Abstraction;
using Core.Abstraction.Repositories;
using Core.Abstraction.Services;
using Core.Abstraction.UseCases.Users;
using Core.DTO;
using Core.DTO.Requests.Users;
using Core.DTO.Responses.Users;
using Core.Models;
using System;
using System.Threading.Tasks;

namespace Core.UseCases.Users
{
    public class CreateUserUseCase : ICreateUserUseCase
    {
        private readonly IUserRepository _repository;
        private readonly IUserEmailManager _userEmailManager;

        public CreateUserUseCase(IUserRepository repository, IUserEmailManager userEmailManager)
        {
            _repository = repository;
            _userEmailManager = userEmailManager;
        }

        public async Task<bool> Handle(CreateUserRequest message, IOutputPort<UserResponse> outputPort)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(message.Email))
                    throw new ArgumentException(nameof(message.Email));

                if (string.IsNullOrWhiteSpace(message.PasswordHash))
                    throw new ArgumentException(nameof(message.PasswordHash));

                if (await _repository.Exists(message.Email))
                    throw new InvalidOperationException("Duplicate user.");

                var result = await _repository.Create(new User(message.Email, message.PasswordHash));

                outputPort.Handle(new UserResponse(new User(result.Id, result.Email, "", result.Blocked)));

                await _userEmailManager.SendConfirmationEmail(result.Id, result.Email);

                return true;
            }
            catch (Exception ex)
            {
                outputPort.Handle(new UserResponse(new[] { new Error("createuser", ex.Message) }));
                return false;
            }

        }

    }
}
