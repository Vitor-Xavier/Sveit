using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sveit.Models;

namespace Sveit.Services.Gender
{
    public class FakeGenderService : IGenderService
    {
        public IEnumerable<Models.Gender> Genders { get; private set; }

        public FakeGenderService()
        {
            Genders = GetFakeGenders();
        }

        private IEnumerable<Models.Gender> GetFakeGenders()
        {
            yield return new Models.Gender { GenderId = 1, Name = "Male" };
            yield return new Models.Gender { GenderId = 2, Name = "Female" };
            yield return new Models.Gender { GenderId = 3, Name = "Others" };
        }

        public Task<bool> AddGenderAsync(Models.Gender gender)
        {
            return Task.FromResult(false);
        }

        public Task<Models.Gender> GetGenderAsync(int genderId)
        {
            return Task.FromResult(Genders.First());
        }

        public Task<IEnumerable<Models.Gender>> GetGendersAsync()
        {
            return Task.FromResult(Genders);
        }

        public Task<IEnumerable<Models.Gender>> GetGendersByNameAsync(string name)
        {
            return Task.FromResult(Genders);
        }

        public Task<bool> RemoveGenderAsync(int genderId)
        {
            return Task.FromResult(false);
        }
    }
}
