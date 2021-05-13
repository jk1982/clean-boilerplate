using Core.DTO.Requests.Users;
using Core.DTO.Responses.Users;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Abstraction.UseCases.Users
{
    public interface IGetUserUseCase : IUseCaseRequestHandler<GetUserRequest, UserResponse>
    {
    }
}
