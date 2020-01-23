using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpRequestHandler
{
    public class ServiceGateway : IServiceGateway
    {
        private readonly ISerializer _serializer;
        private readonly IDeserializer _deserializers;
        private readonly IUploader _uploader;
        private readonly IDeleter _deleter;
        public ServiceGateway(ISerializer serializer, IDeserializer deserializer, IUploader uploader, IDeleter deleter)
        {
            _serializer = serializer;
            _deserializers = deserializer;
            _uploader = uploader;
            _deleter = deleter;
        }

        #region Retrieve
        public T Retrieve<T>(string url, string token = null, List<Dictionary<string, string>> headers = null)
        {
            var stream = GetResponse(url, headers, token).GetResponseStream();
            return Deserialize<T>(stream);
        }

        private HttpWebResponse GetResponse(string url, List<Dictionary<string, string>> headers, string token = null)
        {
            var response = IssueRequest(url, headers, token);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                //handle no content
                throw new Exception();
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                //handle error
                throw new Exception();
            }

            //success

            return response;
        }

        private HttpWebResponse IssueRequest(string url, List<Dictionary<string, string>> headers, string token = null)
        {
            return (HttpWebResponse)CreateHttpWebRequest(url, headers, token).GetResponse();
        }

        protected virtual HttpWebRequest CreateHttpWebRequest(string url, List<Dictionary<string, string>> headers, string token = null)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            if (token != null)
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

            return request;
        }

        private T Deserialize<T>(Stream stream)
        {
            try
            {
                return _deserializers.Deserialize<T>(stream);
            }
            finally
            {
                stream.Dispose();
            }
        }

        #endregion

        #region Retrieve async

        public async Task<T> RetrieveAsync<T>(string url, string token, List<Dictionary<string, string>> headers = null)
        {
            var response = await Task.Run(() =>
            {
                var stream = GetResponse(url, headers, token).GetResponseStream();
                return Deserialize<T>(stream);
            });

            return response;
        }
        #endregion

        #region send

        public void Send<TRequest>(string url, TRequest requestData, List<Dictionary<string, string>> headers = null, string token = null)
        {
            SerializeAndUpload(url, requestData, headers, token);
        }

        private Stream SerializeAndUpload<T>(string url, T requestData, List<Dictionary<string, string>> headers, string token = null)
        {
            try
            {
                var requestStream = _serializer.Serialize(requestData);
                return _uploader.UploadStream(url, requestStream, headers, token);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }


        public TResponse Send<TRequest, TResponse>(string url, TRequest requestData, List<Dictionary<string, string>> headers = null, string token = null)
        {
            try
            {
                var responseStream = SerializeAndUpload(url, requestData, headers, token);
                return Deserialize<TResponse>(responseStream);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        #endregion

        #region Send async

        public async void SendAsync<TRequest>(string url, TRequest requestData, List<Dictionary<string, string>> headers = null, string token = null)
        {
            var response = await Task.Run(() =>
            {
                try
                {
                    return SerializeAndUpload(url, requestData, headers, token);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public async Task<TResponse> SendAsync<TRequest, TResponse>(string url, TRequest requestData, List<Dictionary<string, string>> headers = null, string token = null)
        {
            var response = await Task.Run(() =>
            {
                try
                {
                    var responseStream = SerializeAndUpload(url, requestData, headers, token);
                    return Deserialize<TResponse>(responseStream);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });

            return response;
        }


        #endregion

        #region Update
        public TResponse Update<TRequest, TResponse>(string url, string token, TRequest requestData, List<Dictionary<string, string>> headers = null)
        {
            var responseStream = SerializeAndUpload(url, requestData, headers, token);
            return Deserialize<TResponse>(responseStream);
        }

        #endregion


        #region update async

        public async Task<TResponse> UpdateAsync<TRequest, TResponse>(string url, string token, TRequest requestData, List<Dictionary<string, string>> headers = null)
        {
            var response = await Task.Run(() =>
            {
                try
                {
                    var responseStream = SerializeAndUpload(url,requestData, headers, token);
                    return Deserialize<TResponse>(responseStream);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });

            return response;
        }

        #endregion

        #region Delete

        public TResponse Delete<TResponse>(string url, string token, List<Dictionary<string, string>> headers = null)
        {
            var responseStream = SerializeAndDelete(url, token, headers);
            return Deserialize<TResponse>(responseStream);
        }

        private Stream SerializeAndDelete(string url, string token, List<Dictionary<string, string>> headers)
        {
            return _deleter.DeleteStream(url, token, headers);
        }


        #endregion

        #region Delete async

        public async Task<TResponse> DeleteAsync<TResponse>(string url, string token, List<Dictionary<string, string>> headers = null)
        {
            var response = await Task.Run(() =>
            {
                try
                {
                    var responseStream = SerializeAndDelete(url, token, headers);
                    return Deserialize<TResponse>(responseStream);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });

            return response;
        }

        #endregion

        public string CallSuftiPro(string url, NameValueCollection requestData)
        {
            try
            {
                using (var client = new WebClient())
                {
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                    var response = client.UploadValues(url, requestData);
                    return Encoding.Default.GetString(response);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}
