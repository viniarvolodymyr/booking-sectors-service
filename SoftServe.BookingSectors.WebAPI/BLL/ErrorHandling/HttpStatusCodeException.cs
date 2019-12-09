using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Runtime.Serialization;

namespace SoftServe.BookingSectors.WebAPI.BLL.ErrorHandling
{
    public class HttpStatusCodeException : System.Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ContentType { get; set; } = @"application/json"; // @text/plain

        public HttpStatusCodeException(HttpStatusCode StatusCode)
        {
            this.StatusCode = StatusCode;
        }

        public HttpStatusCodeException(HttpStatusCode StatusCode, string message) : base(message)
        {
            this.StatusCode = StatusCode;
        }

        public HttpStatusCodeException(HttpStatusCode statusCode, System.Exception inner) : this(statusCode, inner.ToString()) 
        { }

        public HttpStatusCodeException(HttpStatusCode statusCode, JObject errorObject) : this(statusCode, errorObject.ToString())
        { }

      #region constructor serialization
        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected HttpStatusCodeException(SerializationInfo info, StreamingContext context)
        {
        }
       #endregion
    }
}
