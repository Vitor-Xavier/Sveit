using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sveit.Extensions;
using Sveit.Models;
using Sveit.Services.Requests;

namespace Sveit.Services.Skill
{
    class SkillService : ISkillService
    {
        private readonly IRequestService _requestService;

        public SkillService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public Task<Models.Skill> GetSkillAsync(int skillId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.SkillsEndpoint);
            builder.AppendToPath(skillId.ToString());
            string uri = builder.ToString();

            return _requestService.GetAsync<Models.Skill>(uri);
        }

        public Task<IEnumerable<Models.Skill>> GetSkillsAsync()
        {
            string uri = AppSettings.SkillsEndpoint;

            return _requestService.GetAsync<IEnumerable<Models.Skill>>(uri);
        }

        public Task<IEnumerable<Models.Skill>> GetSkillsByNameAsync(string name)
        {
            UriBuilder builder = new UriBuilder(AppSettings.SkillsEndpoint);
            builder.AppendToPath("Name");
            builder.AppendToPath(name);
            string uri = builder.ToString();

            return _requestService.GetAsync<IEnumerable<Models.Skill>>(uri);
        }

        public Task<bool> PostSkillAsync(Models.Skill skill)
        {
            string uri = AppSettings.SkillsEndpoint;

            return _requestService.PostAsync<Models.Skill, bool>(uri, skill);
        }

        public Task<bool> DeleteSkillAsync(int skillId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.SkillsEndpoint);
            builder.AppendToPath(skillId.ToString());
            string uri = builder.ToString();

            return _requestService.DeleteAsync(uri, "");
        }
    }
}
