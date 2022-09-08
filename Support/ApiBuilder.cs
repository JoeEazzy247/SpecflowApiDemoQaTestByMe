using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SpecflowApiDemoQaTestByMe.Models;

namespace SpecflowApiDemoQaTestByMe.Support
{
    class ApiBuilder
    {
        private readonly RestClient client;
        private RestRequest request;
        private RestResponse response;


        public ApiBuilder()
        {
            this.client = new RestClient();
            this.request = new RestRequest();
            this.response = new RestResponse();
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

        public ApiBuilder withJsonBody(PostBookModel method)
        {
            request.AddJsonBody(method);
            return this;
        }

        public ApiBuilder withBody(PostBookModel method)
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
    }
}
