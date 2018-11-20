using Sveit.Controls;
using Sveit.Extensions;
using Sveit.Models;
using Sveit.Services.Apply;
using Sveit.Services.Player;
using Sveit.Services.Requests;
using Sveit.Services.Team;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sveit.ViewModels
{
    public class ApplyViewModel : BaseViewModel
    {
        private readonly IApplyService _applyService;

        private readonly IPlayerService _playerService;

        private readonly ITeamService _teamService;

        private readonly INavigation _navigation;

        private bool isEvaluation;

        public bool IsEvaluation
        {
            get { return isEvaluation; }
            set { isEvaluation = value; OnPropertyChanged(); }
        }

        private Apply apply;

        public Apply Apply
        {
            get { return apply; }
            set { apply = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Skill> Skills { get; set; }

        public List<ObservableGroupCollection<RoleType, Role>> RoleTypes { get; set; }

        public IAsyncCommand AcceptCommand => new AsyncCommand(AcceptCommandExecute);

        public IAsyncCommand DeclineCommand => new AsyncCommand(DeclineCommandExecute);

        public ApplyViewModel(INavigation navigation, IRequestService requestService, Apply apply, bool isEvaluation)
        {
            IsEvaluation = isEvaluation;
            if (Apply.Approved != null)
                IsEvaluation = false;
            _navigation = navigation;
            Apply = apply;
            if (AppSettings.ApiStatus)
            {
                _applyService = new ApplyService(requestService);
                _teamService = new TeamService(requestService);
                _playerService = new PlayerService(requestService);
            }
            else
            {
                _applyService = new FakeApplyService();
                _teamService = new FakeTeamService();
                _playerService = new FakePlayerService();
            }
            RoleTypes = Apply.Roles.OrderBy(p => p.Name)
               .GroupBy(p => p.RoleType)
               .Select(p => new ObservableGroupCollection<RoleType, Role>(p)).ToList();
            Skills = new ObservableCollection<Skill>();

            Task.Run(() => LoadData());
        }

        private async Task LoadData()
        {
            var skills = await _playerService.GetSkills(Apply.PlayerId);

            Skills.Clear();
            foreach (Skill s in skills)
                Skills.Add(s);
        }

        private async Task AcceptCommandExecute()
        {
            Apply.Approved = true;
            try
            {
                var result = await _applyService.PostApply(Apply);
                if (result != null)
                {
                    var memberResult = await _teamService.PostTeamPlayer(new TeamPlayer { TeamId = apply.Vacancy.TeamId, PlayerId = result.PlayerId });
                    if (memberResult != null)
                    {
                        await _navigation.PopModalAsync();
                        return;
                    }
                }
                DependencyService.Get<IMessage>().ShortAlert(AppResources.ApproveFailed);
            }
            catch
            {
                DependencyService.Get<IMessage>().ShortAlert(AppResources.ApproveFailed);
            }
        }

        private async Task DeclineCommandExecute()
        {
            Apply.Approved = false;
            try
            {
                var result = await _applyService.PostApply(Apply);
                if (null != result)
                    await _navigation.PopModalAsync();
                else
                    DependencyService.Get<IMessage>().ShortAlert(AppResources.DeclineFailed);
            }
            catch
            {
                DependencyService.Get<IMessage>().ShortAlert(AppResources.DeclineFailed);
            }
            
        }
    }
}
