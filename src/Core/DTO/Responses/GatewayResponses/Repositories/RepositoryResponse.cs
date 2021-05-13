using System;
using System.Collections.Generic;

namespace Core.DTO.Responses.GatewayResponses.Repositories
{
    public sealed class RepositoryResponse<T> : BaseGatewayResponse
    {
        public T Id { get; }

        public RepositoryResponse(T id, bool success = false, IEnumerable<Error> errors = null) : base(success, errors)
        {
            Id = id;
        }
    }
}
