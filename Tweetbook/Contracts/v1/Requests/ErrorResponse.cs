﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tweetbook.Contracts.v1.Requests
{
    public class ErrorResponse
    {
        public List<ErrorModel> Errors { get; set; }
    }
}
