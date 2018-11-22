using Sveit.Controls;
using Sveit.Extensions;
using Sveit.Models;
using Sveit.Services.Gender;
using Sveit.Services.Requests;
using Sveit.Services.Role;
using Sveit.Services.Skill;
using Sveit.Services.Vacancy;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.MultiSelectListView;

namespace Sveit.ViewModels
{
    public class VacancyRegisterViewModel : BaseViewModel
    {
        private readonly int _vacancyId;

        private readonly INavigation _navigation;

        private readonly IVacancyService _vacancyService;

        private readonly ISkillService _skillService;

        private readonly IGenderService _genderService;

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

        public ObservableCollection<Skill> Skills { get; private set; }

        public MultiSelectObservableCollection<Gender> Genders { get; set; }

        public List<MultiSelectObservableGroupCollection<RoleType, Role>> RoleTypes { get; set; }

        public IAsyncCommand FinalizeCommand => new AsyncCommand(FinalizeCommandExecute);

        public VacancyRegisterViewModel(INavigation navigation, IRequestService requestService, Team team, int vacancyId)
        {
            _vacancyId = vacancyId;
            _navigation = navigation;
            Team = team;
            if (AppSettings.ApiStatus)
            {
                _vacancyService = new VacancyService(requestService);
                _roleService = new RoleService(requestService);
                _skillService = new SkillService(requestService);
                _genderService = new GenderService(requestService);
            }
            else
            {
                _vacancyService = new FakeVacancyService();
                _roleService = new FakeRoleService();
                _genderService = new FakeGenderService();
            }

            Genders = new MultiSelectObservableCollection<Gender>();
            Skills = new ObservableCollection<Skill>();

            Task.Run(async () =>
            {
                await LoadRoles();
                await LoadGenders();
            });
            Task.Run(() => LoadVacancy());
        }

        public Skill ValidateAndReturn(string tag)
        {
            if (string.IsNullOrWhiteSpace(tag))
                return null;

            return new Skill()
            {
                Name = tag
            };
        }

        private async Task LoadRoles()
        {
            var roles = await _roleService.GetRolesAsync();

            if (roles != null)
                RoleTypes = roles.OrderBy(p => p.Name)
                    .GroupBy(p => p.RoleType)
                    .Select(p => new MultiSelectObservableGroupCollection<RoleType, Role>(p)).ToList();
        }

        private async Task LoadGenders()
        {
            var genders = await _genderService.GetGendersAsync();

            Genders.Clear();
            foreach (Gender g in genders)
                Genders.Add(g);
        }

        private async Task FinalizeCommandExecute()
        {
            if (!FinalizeCommandCanExecute())
            {
                DependencyService.Get<IMessage>().ShortAlert(AppResources.InvalidValues);
                return;
            }
            var roles = new List<Role>();
            foreach (var group in RoleTypes)
                roles.AddRange(group.SelectedItems);

            var genders = new List<Gender>(Genders.SelectedItems);

            var vacancy = new Vacancy
            {
                VacancyId = _vacancyId,
                Title = Title,
                Description = Description,
                MinAge = MinAge,
                MaxAge = MaxAge,
                Roles = roles,
                Skills = Skills,
                Genders = genders,
                TeamId = Team.TeamId,
                Available = true
            };

            try
            {
                var result = await _vacancyService.PostVacancyAsync(vacancy);
                if (result != null)
                    await _navigation.PopModalAsync();
                else
                    DependencyService.Get<IMessage>().ShortAlert(AppResources.VacancyFailed);
            }
            catch
            {
                DependencyService.Get<IMessage>().ShortAlert(AppResources.VacancyFailed);
            }
        }

        private bool FinalizeCommandCanExecute()
        {
            if (string.IsNullOrWhiteSpace(Title) || Title.Length < 4)
                return false;
            if (string.IsNullOrWhiteSpace(Description) || Description.Length < 20)
                return false;
            return true;
        }

        private async Task LoadVacancy()
        {
            if (_vacancyId == 0) return;

            var vacancy = await _vacancyService.GetVacancyAsync(_vacancyId);
            if (vacancy == null) return;
            Title = vacancy.Title;
            Description = vacancy.Description;
            MinAge = vacancy.MinAge;
            MaxAge = vacancy.MaxAge;

            foreach (Skill sk in vacancy.Skills)
                Skills.Add(sk);
            foreach (var m in RoleTypes)
            {
                var selectedItems = m.Where(s => vacancy.Roles.Any(r => (s.Data as Role).RoleId == r.RoleId));
                foreach (SelectableItem s in selectedItems)
                    s.IsSelected = true;
            }
            foreach (SelectableItem s in Genders)
            {
                if (vacancy.Genders.Any(g => g.GenderId == (s.Data as Gender).GenderId))
                    s.IsSelected = true;
            }
        }
    }
}
