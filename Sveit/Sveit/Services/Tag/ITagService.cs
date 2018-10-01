using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sveit.Services.Tag
{
    interface ITagService
    {
        Task<Models.Tag> GetTag(int tagId);

        Task<IEnumerable<Models.Tag>> GetTagsAsync();

        Task<IEnumerable<Models.Tag>> GetTagsByNameAsync(string name);

        Task<bool> PostTagAsync(Models.Tag tag);

        Task<bool> DeleteTagAsync(int tagId);
    }
}
