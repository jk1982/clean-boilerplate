using Core.Abstraction;
using Core.DTO.Responses.Users;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Presenters.Users
{
    public class GetUserPresenter : IOutputPort<UserResponse>
    {
        public bool Succeeded { get; private set; }

        public JsonContentResult ContentResult { get; }
        public GetUserPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(UserResponse response)
        {
            this.Succeeded = response != null;

            ContentResult.StatusCode = (int)(response != null ? HttpStatusCode.OK : HttpStatusCode.NotFound);
            ContentResult.Content = JsonSerializer.SerializeObject(response);
        }
    }
}
