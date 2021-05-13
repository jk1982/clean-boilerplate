using Core.Abstraction;
using Core.DTO.Responses.Users;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTO.Requests.Users
{
    public class GetUserRequest : IUseCaseRequest<UserResponse>
    {
        public GetUserRequest(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
