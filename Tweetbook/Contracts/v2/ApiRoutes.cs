using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tweetbook.Contracts.v2
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v2";
        //public static readonly string Base = $"{Root}/{Version}";
        public const string Base = "api/v2";


        public static class Posts
        {
            //public static readonly string GetAll = $"{Base}/posts";
            public const string GetAll = "/api/v2/posts";
        }
    }
}
