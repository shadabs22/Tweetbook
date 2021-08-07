using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetbook.Contracts.v1.Responses;
using Tweetbook.Domain.v1;

namespace Tweetbook.MappingProfiles
{
    public class DomainToResponseProfile : Profile
    {

        public DomainToResponseProfile()
        {
            CreateMap<Post, PostResponse>()
                .ForMember(dest => dest.tags, opt => opt.MapFrom(src => src.tags.Select(x => new TagResponse { text = x.text })));
            CreateMap<Tag, TagResponse>();
        }
    }
}
