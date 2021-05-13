using Microsoft.AspNetCore.Mvc;

namespace API
{
    public class JsonContentResult : ContentResult
    {
        public JsonContentResult()
        {
            this.ContentType = "application/json";
        }
    }
}