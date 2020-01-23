using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace HttpRequestHandler
{
    public class Uploader : IUploader
    {
        public Stream UploadStream(string url, Stream stream, List<Dictionary<string, string>> headers=null, string token=null)
        {
            return GetResponse(url, stream, headers,token).GetResponseStream();
        }

        private HttpWebResponse GetResponse(string url, Stream stream, List<Dictionary<string, string>> headers = null, string token = null)
        {
            var request = CreateAndSetupRequest(url, stream, headers,token);
           

            var response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception();
            }

            return response;
        }

        private HttpWebRequest CreateAndSetupRequest(string url, Stream stream, List<Dictionary<string, string>> headers = null, string token = null)
        {
            var request = CreateHttpWebRequest(url);
            if(!string.IsNullOrWhiteSpace(token))
            {
                request.Headers["Authorization"] = "Bearer " + token;
            }


            if (headers != null && headers.Count > 0)
            {
                foreach (var dictionary in headers)
                {
                    foreach (var keyValue in dictionary)
                    {
                        request.Headers[keyValue.Key] = keyValue.Value;
                    }
                }
            }

            var content = ReadStream(stream);
            request.ContentLength = content.Length;
            request.Method = "POST";
            request.ContentType = "application/json";
            var requestStream = request.GetRequestStream();
            requestStream.Write(content, 0, content.Length);

            return request;
        }

        protected virtual HttpWebRequest CreateHttpWebRequest(string url)
        {
            return (HttpWebRequest)WebRequest.Create(url);
        }

        private static byte[] ReadStream(Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.Position = 0;
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
