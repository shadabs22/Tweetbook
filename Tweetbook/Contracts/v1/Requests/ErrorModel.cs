using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tweetbook.Contracts.v1.Requests
{
    public class ErrorModel
    {
        public string FiledName { get; set; }
        public string Message { get; set; }
    }
}
