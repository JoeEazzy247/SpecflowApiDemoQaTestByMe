using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecflowApiDemoQaTestByMe
{
    public class Models
    {
        public class Env
        {
            public BaseUrl baseUrl { get; set; }
            public EndPoints endpoints { get; set; }
        }

        public class BaseUrl
        {
            public string baseurl { get; set; }
        }

        public class EndPoints
        {
            public string getallbooks { get; set; }
            public string addbook { get; set; }
            public string deleteallbook { get; set; }
            public string getsinglebook { get; set; }
            public string deletesinglebook { get; set; }
            public string modifybookbyisbn { get; set; }
            public string authoriseAccount { get; set; }
            public string generateToken { get; set; }
            public string addnewUser { get; set; }
            public string deleteUserbyUserId { get; set; }
            public string getUserbyUserid { get; set; }
        }

        public class BooksResponseModel
        {
            public List<BooksModel> books { get; set; }
        }
        public class BooksModel
        {
            public string isbn { get; set; }
            public string title { get; set; }
            public string subTitle { get; set; }
            public string author { get; set; }
            public DateTime publish_date { get; set; }
            public string publisher { get; set; }
            public int pages { get; set; }
            public string description { get; set; }
            public string website { get; set; }
        }

        public class CollectOfIsbn
        {
            public string isbn { get; set; }
        }

        public class PostBookModel
        {
            public string userid { get; set; }
            public List<CollectOfIsbn> collectionOfIsbns { get; set; }
        }

        public class UserModel
        {
            public string userName { get; set; }
            public string password { get; set; }
        }

        public class UserResponseModel
        {
            public string userName { get; set; }
            public string userID { get; set; }
            public List<object> books { get; set; }
        }
    }
}
