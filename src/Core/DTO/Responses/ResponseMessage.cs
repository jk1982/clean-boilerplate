namespace Core.DTO.Responses
{
    public abstract class ResponseMessage
    {
        public bool Success { get; }
        public string Message { get; }

        protected ResponseMessage(bool success = false, string message = null)
        {
            Success = success;
            Message = message;
        }

    }
}
