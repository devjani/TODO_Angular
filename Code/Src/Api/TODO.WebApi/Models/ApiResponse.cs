using System.Runtime.Serialization;

namespace TODO.Web.Models
{
    [DataContract]
    public class APIResponse
    {
        [DataMember]
        public int StatusCode { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public ApiError ResponseException { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public object Result { get; set; }


        public APIResponse(int statusCode, string message = "", object result = null, ApiError apiError = null)
        {
            StatusCode = statusCode;
            Message = message;
            Result = result;
            ResponseException = apiError;
        }
    }
}
