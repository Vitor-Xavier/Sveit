using Sveit.Extensions;
using Sveit.Models;
using Sveit.Services.Requests;
using Sveit.Services.Role;
using Sveit.Services.Vacancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sveit.ViewModels
{
    public class VacancyRegisterViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;

        private readonly IVacancyService _vacancyService;

        private readonly IRoleService _roleService;

        private int minAge;

        public int MinAge
        {
            get { return minAge; }
            set { minAge = value; OnPropertyChanged(); }
        }

        private int maxAge;

        public int MaxAge
        {
            get { return maxAge; }
            set { maxAge = value; OnPropertyChanged(); }
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged(); }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged(); }
        }

        private Team team;

        public Team Team
        {
            get { return team; }
            set { team = value; OnPropertyChanged(); }
        }

        public List<MultiSelectObservableGroupCollection<RoleType, Role>> RoleTypes { get; set; }

        public IAsyncCommand FinalizeCommand => new AsyncCommand(FinalizeCommandExecute);

        public VacancyRegisterViewModel(INavigation navigation, IRequestService requestService, Team team)
        {
            _navigation = navigation;
            if (AppSettings.ApiStatus)
            {
                _vacancyService = new VacancyService(requestService);
                _roleService = new RoleService(requestService);
            }
            else
            {
                _vacancyService = new FakeVacancyService();
                _roleService = new FakeRoleService();
            }
                
        }

        private async Task LoadRoles()
        {
            var roles = await _roleService.GetRolesAsync();
                
            RoleTypes = roles.OrderBy(p => p.Name)
               .GroupBy(p => p.RoleType)
               .Select(p => new MultiSelectObservableGroupCollection<RoleType, Role>(p)).ToList();
        }

        //TODO: Add restrictions
        private async Task FinalizeCommandExecute()
        {
            var roles = new List<Role>();
            foreach (var group in RoleTypes)
                roles.AddRange(group.SelectedItems);

            var vacancy = new Vacancy
            {
                Title = Title,
                Description = Description,
                MinAge = MinAge,
                MaxAge = MaxAge,
                Roles = roles,

                TeamId = Team.TeamId,
                Available = true
            };

            var result = await _vacancyService.PostVacancyAsync(vacancy);
            if (result != null)
                await _navigation.PopModalAsync();
        }
    }
}
