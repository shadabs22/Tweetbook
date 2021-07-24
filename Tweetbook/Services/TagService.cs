using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetbook.Data;
using Tweetbook.Domain.v1;

namespace Tweetbook.Services
{
    public class TagService : ITagService
    {
        private readonly DataContext _dataContext;

        public TagService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<bool> CreateTag(Tag newTag)
        {
            await _dataContext.Tags.AddAsync(newTag);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> CreateTags(List<Tag> newTags)
        {
            await _dataContext.Tags.AddRangeAsync(newTags);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> DeleteTag(Guid tagId)
        {
            throw new NotImplementedException();
        }

        public async Task<Tag> GetTagById(Guid tagId)
        {
            return _dataContext.Tags.SingleOrDefault(x => x.id == tagId);
        }

        public async Task<List<Tag>> GetTags()
        {
            return _dataContext.Tags.ToList();
        }

        public async Task<bool> UpdateTag(Tag tag)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UserOwnTagAsync(Guid tagId, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
