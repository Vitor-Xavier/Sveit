using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sveit.Services.Apply
{
    public interface IApplyService
    {
        Task<Models.Apply> GetApply(int applyId);

        Task<IEnumerable<Models.Apply>> GetAppliesByVacancy(int vacancyId);

        Task<IEnumerable<Models.Apply>> GetAppliesByPlayer(int playerId);

        Task<Models.Apply> PostApply(Models.Apply apply);

        Task<bool> DeleteApply(int applyId);
    }
}
