﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetbook.Domain.v1;

namespace Tweetbook.Contracts.v1.Requests
{
    public class CreatePostRequest
    {
        public string name { get; set; }

        public List<TagPostRequest> tags { get; set; }
    }
}
