using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sveit.Services.Skill
{
    interface ISkillService
    {
        Task<Models.Skill> GetSkillAsync(int skillId);

        Task<IEnumerable<Models.Skill>> GetSkillsAsync();

        Task<IEnumerable<Models.Skill>> GetSkillsByNameAsync(string name);

        Task<bool> PostSkillAsync(Models.Skill skill);

        Task<bool> DeleteSkillAsync(int skillId);
    }
}
