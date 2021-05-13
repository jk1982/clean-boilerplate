using Core.Abstraction;
using Core.Abstraction.Repositories;
using Core.Abstraction.Services;
using Core.Abstraction.UseCases.Auth;
using Core.DTO;
using Core.DTO.Requests.Auth;
using Core.DTO.Responses.Auth;
using System;
using System.Threading.Tasks;

namespace Core.UseCases.Auth
{
    public class LoginUseCase : ILoginUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtFactory _jwtFactory;

        public LoginUseCase(IUserRepository userRepository, IJwtFactory jwtFactory)
        {
            _userRepository = userRepository;
            _jwtFactory = jwtFactory;
        }

        public async Task<bool> Handle(LoginRequest message, IOutputPort<LoginResponse> outputPort)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(message.Email))
                    throw new ArgumentNullException(nameof(message.Email));

                if (string.IsNullOrWhiteSpace(message.PasswordHash))
                    throw new ArgumentNullException(nameof(message.PasswordHash));

                var user = await _userRepository.GetBy(message.Email);

                if (user == null || await _userRepository.Validate(user.Email, message.PasswordHash) == false)
                    throw new ApplicationException("Invalid email or password.");

                var token = await _jwtFactory.GenerateAuthToken(user.Id, user.Email);

                if (token == null)
                    throw new ArgumentNullException(nameof(token));

                outputPort.Handle(new LoginResponse(token));

                return true;
            }
            catch (Exception ex)
            {
                outputPort.Handle(new LoginResponse(new[] { new Error("login", ex.Message) }));
                return false;
            }
        }
    }
}
