using Sveit.Controls;
using Sveit.Extensions;
using Sveit.Models;
using Sveit.Services.Gender;
using Sveit.Services.Requests;
using Sveit.Services.Role;
using Sveit.Services.Skill;
using Sveit.Services.Vacancy;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.MultiSelectListView;

namespace Sveit.ViewModels
{
    public class VacancyRegisterViewModel : BaseViewModel
    {
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

        public VacancyRegisterViewModel(INavigation navigation, IRequestService requestService, Team team)
        {
            _navigation = navigation;
            if (AppSettings.ApiStatus)
            {
                _vacancyService = new VacancyService(requestService);
                _roleService = new RoleService(requestService);
                _skillService = new SkillService(requestService);
            }
            else
            {
                _vacancyService = new FakeVacancyService();
                _roleService = new FakeRoleService();
            }

            Genders = new MultiSelectObservableCollection<Gender>();
            Skills = new ObservableCollection<Skill>();
            Skills.CollectionChanged += SkillCollectionChanged;

            Task.Run(async () =>
            {
                await LoadRoles();
                await LoadGenders();
            });
        }

        private async void SkillCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                //TODO: Check if Skills are being added
                //if (e.NewItems != null)
                //{
                //    var skill = e.NewItems.Cast<Skill>().First();
                //    await _skillService.PostSkillAsync(skill);
                //}
            }
        }

        private async Task LoadRoles()
        {
            var roles = await _roleService.GetRolesAsync();
                
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

            var result = await _vacancyService.PostVacancyAsync(vacancy);
            if (result != null)
                await _navigation.PopModalAsync();
        }

        private bool FinalizeCommandCanExecute()
        {
            if (string.IsNullOrWhiteSpace(Title) || Title.Length < 4)
                return false;
            if (string.IsNullOrWhiteSpace(Description) || Description.Length < 20)
                return false;
            return true;
        }
    }
}
