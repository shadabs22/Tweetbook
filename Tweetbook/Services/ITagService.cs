using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetbook.Domain.v1;

namespace Tweetbook.Services
{
    public interface ITagService
    {
        Task<List<Tag>> GetTags();
        Task<Tag> GetTagById(Guid tagId);
        Task<bool> CreateTag(Tag newTag);
        Task<bool> CreateTags(List<Tag> newTags);
        Task<bool> UpdateTag(Tag tag);
        Task<bool> DeleteTag(Guid tagId);
        Task<bool> UserOwnTagAsync(Guid tagId, string userId);
    }
}
