using Core.Abstraction;
using Core.DTO.Responses.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Presenters.Auth
{
    public class LoginPresenter : IOutputPort<LoginResponse>
    {
        public bool Succeeded { get; private set; }

        public JsonContentResult ContentResult { get; }
        public LoginPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(LoginResponse response)
        {
            this.Succeeded = response.Success;

            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = JsonSerializer.SerializeObject(response);
        }
    }
}
