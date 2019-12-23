using Microsoft.AspNetCore.Http;

namespace SoftServe.BookingSectors.WebAPI.Extensions
{
    public static class RequestExtenstion
    {
        public static string BodyToString(this HttpRequest request)
        {
            using (var reader = new System.IO.StreamReader(request.Body))
            {
                return reader.ReadToEnd();
            }
        }
    }
}