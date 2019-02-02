using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sveit.Services.Content
{
    public interface IContentService
    {
        Task<IEnumerable<Models.Content>> GetContentsAsync(DateTime? initialDate = null, DateTime? finalDate = null);

        Task<bool> PostContentAsync(Models.Content content);

        Task<bool> DeleteContentAsync(int contentId);
    }
}
