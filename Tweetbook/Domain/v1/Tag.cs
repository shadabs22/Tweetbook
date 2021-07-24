using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tweetbook.Domain.v1
{
    public class Tag
    {
        public Guid id { get; set; }
        public string text { get; set; }
        public Guid postid { get; set; }
        public string userid { get; set; }

        [ForeignKey(nameof(postid))]
        public Post post { get; set; }

        [ForeignKey(nameof(userid))]
        public IdentityUser User { get; set; }
    }
}
