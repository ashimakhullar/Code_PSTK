/* 
 * Swagger Server
 *
 *Ashima Bahl
 *
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using RestSharp;
using IO.Swagger.Client;
using IO.Swagger.Model;
using System.Threading.Tasks;

namespace IO.Swagger.Api
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IAAAApi : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// HX Installation information
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="acceptLanguage"> (optional)</param>
        /// Returns a a JSON formatted envelope which contains encrypted JWT [JSON Web Token] which contains authorization information.
        /// <returns>TokenInfo</returns>
        TokenInfo OpTokenInfoPost(string acceptLanguage = null);

        /// <summary>
        /// HX Installation information
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="acceptLanguage"> (optional)</param>
        /// <returns>ApiResponse of AboutInfo</returns>
        ApiResponse<TokenInfo> OpTokenInfoPostWithHttpInfo (string acceptLanguage = null);
        #endregion Synchronous Operations
        #region Asynchronous Operations
        /// <summary>
        /// HX Installation information
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="acceptLanguage"> (optional)</param>
        /// <returns>Task of AboutInfo</returns>
        System.Threading.Tasks.Task<TokenInfo> OpTokenInfoPostAsync(string acceptLanguage = null);

        /// <summary>
        /// HX Installation information
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="acceptLanguage"> (optional)</param>
        /// <returns>Task of ApiResponse (AboutInfo)</returns>
        System.Threading.Tasks.Task<ApiResponse<TokenInfo>> OpTokenInfoPostAsyncWithHttpInfo(string acceptLanguage = null);
        #endregion Asynchronous Operations
    }

    public class TokenInfo
    {
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class AAAApi : IAAAApi
    {
        private IO.Swagger.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenApi"/> class.
        /// </summary>
        /// <returns></returns>
        public AAAApi(String basePath)
        {
            this.Configuration = new Configuration(new ApiClient(basePath));

            ExceptionFactory = IO.Swagger.Client.Configuration.DefaultExceptionFactory;

            // ensure API client has configuration ready
            if (Configuration.ApiClient.Configuration == null)
            {
                this.Configuration.ApiClient.Configuration = this.Configuration;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AboutApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public AAAApi(Configuration configuration = null)
        {
            if (configuration == null) // use the default one in Configuration
                this.Configuration = Configuration.Default;
            else
                this.Configuration = configuration;

            ExceptionFactory = IO.Swagger.Client.Configuration.DefaultExceptionFactory;

            // ensure API client has configuration ready
            if (Configuration.ApiClient.Configuration == null)
            {
                this.Configuration.ApiClient.Configuration = this.Configuration;
            }
        }

        /// <summary>
        /// Gets the base path of the API client.
        /// </summary>
        /// <value>The base path</value>
        public String GetBasePath()
        {
            return this.Configuration.ApiClient.RestClient.BaseUrl.ToString();
        }

        /// <summary>
        /// Sets the base path of the API client.
        /// </summary>
        /// <value>The base path</value>
        [Obsolete("SetBasePath is deprecated, please do 'Configuration.ApiClient = new ApiClient(\"http://new-path\")' instead.")]
        public void SetBasePath(String basePath)
        {
            // do nothing
        }

        /// <summary>
        /// Gets or sets the configuration object
        /// </summary>
        /// <value>An instance of the Configuration</value>
        public Configuration Configuration {get; set;}

        /// <summary>
        /// Provides a factory method hook for the creation of exceptions.
        /// </summary>
        public IO.Swagger.Client.ExceptionFactory ExceptionFactory
        {
            get
            {
                if (_exceptionFactory != null && _exceptionFactory.GetInvocationList().Length > 1)
                {
                    throw new InvalidOperationException("Multicast delegate for ExceptionFactory is unsupported.");
                }
                return _exceptionFactory;
            }
            set { _exceptionFactory = value; }
        }

        /// <summary>
        /// Gets the default header.
        /// </summary>
        /// <returns>Dictionary of HTTP header</returns>
        [Obsolete("DefaultHeader is deprecated, please use Configuration.DefaultHeader instead.")]
        public Dictionary<String, String> DefaultHeader()
        {
            return this.Configuration.DefaultHeader;
        }

        /// <summary>
        /// Add default header.
        /// </summary>
        /// <param name="key">Header field name.</param>
        /// <param name="value">Header field value.</param>
        /// <returns></returns>
        [Obsolete("AddDefaultHeader is deprecated, please use Configuration.AddDefaultHeader instead.")]
        public void AddDefaultHeader(string key, string value)
        {
            this.Configuration.AddDefaultHeader(key, value);
        }

        /// <summary>
        /// HX Installation information 
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="acceptLanguage"> (optional)</param>
        /// <returns>AboutInfo</returns>
        public TokenInfo OpTokenInfoPost(string acceptLanguage = null)
        {
             ApiResponse<TokenInfo> localVarResponse = OpTokenInfoPostWithHttpInfo(acceptLanguage);
             return localVarResponse.Data;
        }
        public TokenInfo OpTokenInfoPost(TokenInfo body,string acceptLanguage = null)
        {
            ApiResponse<TokenInfo> localVarResponse = OpTokenInfoPostWithHttpInfo(body,acceptLanguage);
            return localVarResponse.Data;
        }


        /// <summary>
        /// HX Installation information 
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="acceptLanguage"> (optional)</param>
        /// <returns>ApiResponse of AboutInfo</returns>
        public ApiResponse< TokenInfo > OpTokenInfoPostWithHttpInfo(string acceptLanguage = null)
        {

            // var localVarPath = "/about";
             var localVarPath = "/about";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new Dictionary<String, String>();
            var localVarHeaderParams = new Dictionary<String, String>(Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            if (acceptLanguage != null) localVarHeaderParams.Add("Accept-Language", Configuration.ApiClient.ParameterToString(acceptLanguage)); // header parameter

            string base64Encoded = GetBase64Encoded();
            localVarHeaderParams.Add("Authorization", "Basic " + base64Encoded);
            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) Configuration.ApiClient.CallApi(localVarPath,
                Method.GET, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("OpAboutGet", localVarResponse);
                if (exception != null) throw exception;
            }
            

            return new ApiResponse<TokenInfo>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (TokenInfo) Configuration.ApiClient.Deserialize(localVarResponse, typeof(TokenInfo)));
        }
        /// <summary>
        /// HX Installation information 
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="acceptLanguage"> (optional)</param>
        /// <returns>ApiResponse of AboutInfo</returns>
        public ApiResponse<TokenInfo> OpTokenInfoPostWithHttpInfo(TokenInfo body,string acceptLanguage = null)
        {

            var localVarPath = "/about";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new Dictionary<String, String>();
            var localVarHeaderParams = new Dictionary<String, String>(Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            if (acceptLanguage != null) localVarHeaderParams.Add("Accept-Language", Configuration.ApiClient.ParameterToString(acceptLanguage)); // header parameter

            string base64Encoded = GetBase64Encoded();
            localVarHeaderParams.Add("Authorization", "Basic " + base64Encoded);
            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse)Configuration.ApiClient.CallApi(localVarPath,
                Method.GET, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int)localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("OpAboutGet", localVarResponse);
                if (exception != null) throw exception;
            }


            return new ApiResponse<TokenInfo>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (TokenInfo)Configuration.ApiClient.Deserialize(localVarResponse, typeof(TokenInfo)));
        }
        /// <summary>
        /// HX Installation information 
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="acceptLanguage"> (optional)</param>
        /// <returns>Task of AboutInfo</returns>
        public async System.Threading.Tasks.Task<TokenInfo> OpTokenInfoPostAsync(string acceptLanguage = null)
        {
             ApiResponse<TokenInfo> localVarResponse = await OpTokenInfoPostAsyncWithHttpInfo(acceptLanguage);
             return localVarResponse.Data;

        }

        //private Task<ApiResponse<TokenInfo>> OpTokenInfoPostAsyncWithHttpInfo(string acceptLanguage)
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// HX Installation information 
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="acceptLanguage"> (optional)</param>
        /// <returns>Task of ApiResponse (AboutInfo)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<TokenInfo>> OpTokenInfoPostAsyncWithHttpInfo(string acceptLanguage = null)
        {

            var localVarPath = "/about";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new Dictionary<String, String>();
            var localVarHeaderParams = new Dictionary<String, String>(Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            if (acceptLanguage != null) localVarHeaderParams.Add("Accept-Language", Configuration.ApiClient.ParameterToString(acceptLanguage)); // header parameter


            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) await Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.GET, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("OpAboutGet", localVarResponse);
                if (exception != null) throw exception;
            }

            return new ApiResponse<TokenInfo>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (TokenInfo) Configuration.ApiClient.Deserialize(localVarResponse, typeof(TokenInfo)));
        }

        private string GetBase64Encoded()
        {
            Configuration.Username = "root";
            Configuration.Password = "Cisco123";
            String userpass = Configuration.Username + ":" + Configuration.Password;
            //String basicAuth = Base64Encode(userpass.ToString());
            byte[] encodedByte = System.Text.ASCIIEncoding.UTF8.GetBytes(userpass);
            string base64Encoded = Convert.ToBase64String(encodedByte);
            return base64Encoded;
        }

        //public TokenInfo OpTokenInfoPost(string acceptLanguage = null)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<ApiResponse<TokenInfo>> OpTokenPostAsyncWithHttpInfo(string acceptLanguage = null)
        //{
        //    throw new NotImplementedException();
        //}

        //public ApiResponse<TokenInfo> OpTokenPostWithHttpInfo(string acceptLanguage = null)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<TokenInfo> OpTokenGetAsync(string acceptLanguage = null)
        //{
        //    throw new NotImplementedException();
        //}

        //public TokenInfo OpTokenInfoGet(string acceptLanguage = null)
        //{
        //    throw new NotImplementedException();
        //}

        //{
        //    throw new NotImplementedException();
        //}        public ApiResponse<TokenInfo> OpTokenGetWithHttpInfo(string acceptLanguage = null)


       

       
    }


}


