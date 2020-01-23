using System;
using System.Collections.Generic;
using System.Net;

namespace TODO.Web.Models
{
    public class ApiException : Exception
    {
        public int StatusCode { get; set; }

        public IEnumerable<ValidationError> Errors { get; set; }

        public string ReferenceErrorCode { get; set; }
        public string ReferenceDocumentLink { get; set; }

        public ApiException(string message,
                            int statusCode = (int)HttpStatusCode.InternalServerError,
                            IEnumerable<ValidationError> errors = null,
                            string errorCode = "",
                            string refLink = "") :
            base(message)
        {
            StatusCode = statusCode;
            Errors = errors;
            ReferenceErrorCode = errorCode;
            ReferenceDocumentLink = refLink;
        }

        public ApiException(Exception ex, int statusCode = (int)HttpStatusCode.InternalServerError) : base(ex.Message)
        {
            StatusCode = statusCode;
        }
    }
}
