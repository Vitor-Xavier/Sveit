using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sveit.Services.Gender
{
    interface IGenderService
    {
        Task<Models.Gender> GetGenderAsync(int genderId);

        Task<IEnumerable<Models.Gender>> GetGendersAsync();

        Task<IEnumerable<Models.Gender>> GetGendersByNameAsync(string name);

        Task<bool> AddGenderAsync(Models.Gender gender);

        Task<bool> RemoveGenderAsync(int genderId);
    }
}
