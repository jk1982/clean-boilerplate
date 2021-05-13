using Core.Abstraction;
using Core.DTO.Responses;
using System.Net;

namespace API.Presenters
{
    public class PersonPresenter : IOutputPort<ServiceResponse>
    {
        public bool Succeeded { get; private set; }

        public JsonContentResult ContentResult { get; }
        public PersonPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(ServiceResponse response)
        {
            this.Succeeded = response.Success;
            
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = JsonSerializer.SerializeObject(response);
        }
    }
}
