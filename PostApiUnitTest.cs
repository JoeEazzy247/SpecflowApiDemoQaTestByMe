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
        UserModel userModel;
        public PostApiTest()
        {
            client = new RestClient(); 
            response = new RestResponse();
            apiBuilder = new ApiBuilder();
            userModel = new UserModel();
        }

        [Test]
        public void CreateNewAccountUserNormal()
        {
            //Create new user
            request = new RestRequest(readConfig.GetBaseUrl + readConfig.AddNewUser, Method.Post);
            request.RequestFormat = DataFormat.Json;
            var rand = new Random().Next(1, 999);
            var payload = new UserModel()
            {
                userName = Credentials.Username + rand,
                password = Credentials.Password1
            };
            request.AddJsonBody(payload);

            var res = client.Execute<UserResponseModel>(request);

            if (res.IsSuccessful == true)
            {
                res.StatusCode.ToString().Should().Be(RespCode.Created.ToString());
                res?.Data?.userName.Length.Should().Be(12);
                res?.Data?.userID.Length.Should().Be(36);
            }
        }

        [Test]
        public void CreateNewAccountUserNormalWithSendRequestMethod()
        {
            //Create new user
            request = new RestRequest(readConfig.GetBaseUrl + readConfig.AddNewUser, Method.Post);
            var rand = new Random().Next(1, 999);
            var payload = new UserModel()
            {
                userName = Credentials.Username + rand,
                password = Credentials.Password2
            };

            response = apiBuilder.SendRequest<UserResponseModel>(readConfig.GetBaseUrl
                + readConfig.AddNewUser,Method.Post, payload);

            UserResponseModel responseModel =
                JsonConvert.DeserializeObject<UserResponseModel>(response.Content);
            if (response.IsSuccessful == true)
            {
                response.StatusCode.ToString().Should().Be(RespCode.Created.ToString());
                responseModel.userName.Length.Should().Be(12);
                responseModel.userID.Length.Should().Be(36);
            }
        }

        [Test]
        public void CreateNewAccountUserWithChainMethod()
        {
            //Create new user
            var rand = new Random().Next(1, 999);
            var data = new UserModel()
            {
                userName = Credentials.Username + rand,
                password = Credentials.Password3
            };
            var json = JsonConvert.SerializeObject(data);

            
            response = apiBuilder.withClient(readConfig.GetBaseUrl + readConfig.AddNewUser)
                .withMethod(Method.Post).withBody(data).Build<UserResponseModel>();

            UserResponseModel responseModel = 
                JsonConvert.DeserializeObject<UserResponseModel>(apiBuilder.response.Content);
            if (apiBuilder.response.IsSuccessful == true)
            {
                apiBuilder.response.StatusCode.ToString().Should().Be(RespCode.Created.ToString());
                responseModel.userName.Length.Should().Be(8);
                responseModel.userID.Length.Should().Be(36);
            }
        } 
    }
}
