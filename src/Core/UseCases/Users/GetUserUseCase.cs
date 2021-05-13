using Core.Abstraction;
using Core.Abstraction.Repositories;
using Core.Abstraction.UseCases.Users;
using Core.DTO;
using Core.DTO.Requests.Users;
using Core.DTO.Responses.GatewayResponses.Repositories;
using Core.DTO.Responses.Users;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.UseCases.Users
{
    public class GetUserUseCase : IGetUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public GetUserUseCase(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task<bool> Handle(GetUserRequest message, IOutputPort<UserResponse> outputPort)
        {
            try
            {
                if (message.Id <= 0)
                    throw new ArgumentException(nameof(message.Id));
                                
                var user = await _userRepository.GetBy(message.Id);

                if (user.Blocked)
                    throw new Exception("User is blocked. Verify your email for confirmation.");

                outputPort.Handle(new UserResponse(user));

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
