using Core.Abstraction;
using Core.DTO.Responses.Users;
using System.Net;

namespace API.Presenters.Users
{
    public class CreateUserPresenter : IOutputPort<UserResponse>
    {
        public bool Succeeded { get; private set; }
        public int Id { get; private set; }

        public JsonContentResult ContentResult { get; }
        public CreateUserPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(UserResponse response)
        {
            this.Succeeded = response.Success;

            if (response.Success)
                this.Id = response.User.Id;

            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = JsonSerializer.SerializeObject(response);
        }
    }
}
