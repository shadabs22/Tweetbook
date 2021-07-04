using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tweetbook.Contracts.v1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        //public static readonly string Base = $"{Root}/{Version}";
        public const string Base = "api/v1";


        public static class Posts
        {
            //public static readonly string GetAll = $"{Base}/posts";
            public const string GetAll = "/api/v1/posts";
            public const string Get = "/api/v1/posts/{postId}";
            public const string Create = "/api/v1/posts";
        }
    }
}
