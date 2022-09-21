using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SpecflowApiDemoQaTestByMe.Models;

namespace SpecflowApiDemoQaTestByMe.Support
{
    public enum RespCode
    {
        OK,
        Created
    }

    public class ApiBuilder
    {
        public RestClient client;
        public RestRequest request;
        public RestResponse response;


        public ApiBuilder()
        {
            client = new RestClient();
            request = new RestRequest();
            response = new RestResponse();
        }

        public ApiBuilder withClient(string url)
        {
            request.Resource = url;
            return this;
        }

        public ApiBuilder withMethod(Method method)
        {
            request.Method = method;
            return this;
        }

        public ApiBuilder withJsonBody(object body)
        {
            request.AddJsonBody(body);
            return this;
        }

        public ApiBuilder withBody(object method)
        {
            request.AddBody(method);
            return this;
        }

        public ApiBuilder withHeader(Dictionary<string, string> headers)
        {
            foreach (var header in headers)
            {
                request.AddHeader(header.Key, header.Value);
            }
            return this;
        }

        public ApiBuilder withUserId(Dictionary<string, string> UserIds)
        {
            foreach (var userid in UserIds)
            {
                request.AddHeader(userid.Key, userid.Value);
            }
            return this;
        }

        public ApiBuilder withToken(string user, string pass)
        {
            request.AddHeader("authorization", 
                $"Basic {GenerateToken(user, pass)}");
            return this;
        }

        public string GenerateToken(string user, string pass)
        {
            byte[] token = Encoding.ASCII.GetBytes($"{user}:{pass}");
            return Convert.ToBase64String(token);
        }

        public RestResponse Build<T>() where T : class
        {
            return response = client.Execute<T>(request);
        }

        public RestResponse SendRequest<T>(string url, Method method,
            object body = null, Dictionary<string, string> param = null, 
            Dictionary<string, string> userId = null, 
            Dictionary<string, string> header = null) where T : class
        {
            client = new RestClient();
            request = new RestRequest(url, method);
            //var reqBody = body != null ? request.AddBody(body) : null;
            if (body != null)
            {
                request.AddBody(body);
            }

            if (param != null)
            {
                foreach (var key in param.Keys)
                {
                    request.AddParameter(key, param[key]);
                }
            }

            if (userId != null)
            {
                foreach (var key in userId.Keys)
                {
                    request.AddParameter(key, userId[key]);
                }
            }

            if (header != null)
            {
                foreach (var key in header.Keys)
                {
                    request.AddParameter(key, header[key]);
                }
            }

            return client.ExecuteAsync<T>(request).Result;
        }
    }
}
