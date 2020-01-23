namespace HttpRequestHandler
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Threading.Tasks;

    public interface IServiceGateway
    {
        /// <summary>
        /// get method
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="url">api url</param>
        /// <param name="token">The token.</param>
        /// <param name="headers">The headers.</param>
        /// <returns>Type object</returns>
        T Retrieve<T>(string url, string token = null, List<Dictionary<string, string>> headers = null);

        /// <summary>
        /// async get method
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="url">api url</param>
        /// <param name="token">The token.</param>
        /// <param name="headers">The headers.</param>
        /// <returns>
        /// Task type
        /// </returns>
        Task<T> RetrieveAsync<T>(string url, string token, List<Dictionary<string, string>> headers = null);

        /// <summary>
        /// post method with null response type
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <param name="url">post api url</param>
        /// <param name="requestData">post request data</param>
        /// <param name="headers">The headers.</param>
        /// <param name="token">The token.</param>
        void Send<TRequest>(string url, TRequest requestData, List<Dictionary<string, string>> headers = null, string token = null);

        /// <summary>
        /// post method with null response type
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <param name="url">post api url</param>
        /// <param name="requestData">post request data</param>
        /// <param name="headers">The headers.</param>
        /// <param name="token">The token.</param>
        void SendAsync<TRequest>(string url, TRequest requestData, List<Dictionary<string, string>> headers = null, string token = null);

        /// <summary>
        /// post method with response type
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="url">post api url</param>
        /// <param name="requestData">post request data</param>
        /// <param name="headers">The headers.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// T response
        /// </returns>
        TResponse Send<TRequest, TResponse>(string url, TRequest requestData, List<Dictionary<string, string>> headers = null, string token = null);

        /// <summary>
        /// async post method with response type
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="url">The URL.</param>
        /// <param name="requestData">The request data.</param>
        /// <param name="headers">The headers.</param>
        /// <param name="token">The token.</param>
        /// <returns>aysnc task</returns>
        Task<TResponse> SendAsync<TRequest, TResponse>(string url, TRequest requestData, List<Dictionary<string, string>> headers = null, string token = null);

        /// <summary>
        /// put method with response
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <param name="requestData"></param>
        /// <returns></returns>
        TResponse Update<TRequest, TResponse>(string url, string token, TRequest requestData, List<Dictionary<string, string>> headers = null);

        /// <summary>
        /// async put method with response
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <param name="requestData"></param>
        /// <returns></returns>
        Task<TResponse> UpdateAsync<TRequest, TResponse>(string url, string token, TRequest requestData, List<Dictionary<string, string>> headers = null);

        /// <summary>
        /// delete method with response
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <param name="requestData"></param>
        /// <returns></returns>
        TResponse Delete<TResponse>(string url, string token, List<Dictionary<string, string>> headers = null);

        /// <summary>
        /// async delete method
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<TResponse> DeleteAsync<TResponse>(string url, string token, List<Dictionary<string, string>> headers = null);

        string CallSuftiPro(string url, NameValueCollection requestData);
    }
}
