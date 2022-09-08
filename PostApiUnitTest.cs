using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using SpecflowApiDemoQaTestByMe.Support;
using static SpecflowApiDemoQaTestByMe.Models;

namespace SpecflowApiDemoQaTestByMe
{
    public class PostApiTest
    {
        RestClient client;
        RestRequest request;
        RestResponse response;
        ApiBuilder apiBuilder;
        BooksResponseModel booksResponse;
        public PostApiTest()
        {
            client = new RestClient(); 
            response = new RestResponse();
            apiBuilder = new ApiBuilder();
        }

        [Test]
        public void CreateNewAccountUser()
        {
            request = new RestRequest(readConfig.GetBaseUrl 
                + readConfig.GetAllBooksEnpoint,
                Method.Get);
            response = client.Execute<BooksResponseModel>(request);
            if (response.IsSuccessful == true)
            {
                response.StatusCode.ToString().Should().Be("OK");
            }
        }

        [Test]
        public void GetBooksTestWithChainMethod()
        {
            //Send a GET request to retrieve a all books
            response = apiBuilder.withClient(readConfig.GetBaseUrl
                + readConfig.GetAllBooksEnpoint)
                .withMethod(Method.Get).Build<BooksResponseModel>();
            var isbn = 
                JsonConvert.DeserializeObject<BooksResponseModel>(
                    response.Content).books.First().isbn;
            if (response.IsSuccessful == true)
            {
                response.StatusCode.ToString().Should().Be("OK");
            }

            //Send a GET request to retrieve a single book with isbn with Chain method
            var 
            resp = apiBuilder.withClient(readConfig.GetBaseUrl
                + string.Format(readConfig.GetSingleBookEnpoint, isbn))
                .withMethod(Method.Get).Build<BooksResponseModel>();
            if (response.IsSuccessful == true)
            {
                response.StatusCode.ToString().Should().Be("OK");
            }
        }
    }
}
