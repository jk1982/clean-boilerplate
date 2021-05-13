using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTO.Responses
{
    public class ServiceResponse : ResponseMessage
    {        
        public IEnumerable<Error> Errors { get; }

        public ServiceResponse(bool success, string message = null) : base(success, message)
        {
        }

        public ServiceResponse(IEnumerable<Error> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }
    }

    public class ServiceResponse<T> : ServiceResponse
    {
        public dynamic Id { get; }

        public ServiceResponse(dynamic id, bool success = false, string message = null) : base(success, message)
        {
            Id = id;
        }
    }
}
