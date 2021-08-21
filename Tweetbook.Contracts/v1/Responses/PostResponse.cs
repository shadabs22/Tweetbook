using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tweetbook.Contracts.v1.Responses
{
    public class PostResponse
    {
        public Guid id { get; set; }

        public string name { get; set; }

        public List<TagResponse> tags { get; set; }

        public string UserId { get; set; }

    }
}
