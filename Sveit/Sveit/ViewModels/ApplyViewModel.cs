using Sveit.Models;
using Sveit.Services.Apply;
using Sveit.Services.Player;
using Sveit.Services.Requests;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sveit.ViewModels
{
    public class ApplyViewModel : BindableObject
    {
        private readonly IApplyService _applyService;

        private readonly IPlayerService _playerService;

        private readonly INavigation _navigation;

        private bool isEvaluation;

        public bool IsEvaluation
        {
            get { return isEvaluation; }
            set { isEvaluation = value; }
        }


        private Apply apply;

        public Apply Apply
        {
            get { return apply; }
            set { apply = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Skill> Skills { get; set; }

        public List<ObservableGroupCollection<RoleType, Role>> RoleTypes { get; set; }

        public ICommand AcceptCommand => new Command(AcceptCommandExecute);

        public ICommand DeclineCommand => new Command(DeclineCommandExecute);

        public ApplyViewModel(INavigation navigation, Apply apply, bool isEvaluation)
        {
            IsEvaluation = isEvaluation;
            _navigation = navigation;
            Apply = apply;
            if (AppSettings.ApiStatus)
            {
                _applyService = new ApplyService(new RequestService());
                _playerService = new PlayerService(new RequestService());
            }
            else
            {
                _applyService = new FakeApplyService();
                _playerService = new FakePlayerService();
            }
            RoleTypes = Apply.Roles.OrderBy(p => p.Name)
               .GroupBy(p => p.RoleType)
               .Select(p => new ObservableGroupCollection<RoleType, Role>(p)).ToList();
            Skills = new ObservableCollection<Skill>();
            LoadData();
        }

        private async void LoadData()
        {
            var skills = await _playerService.GetSkills(Apply.PlayerId);

            Skills.Clear();
            foreach (Skill s in skills)
                Skills.Add(s);
        }

        private async void AcceptCommandExecute()
        {
            Apply.Approved = true;
            if (null != await _applyService.PostApply(Apply))
                await _navigation.PopModalAsync();
        }

        private async void DeclineCommandExecute()
        {
            Apply.Approved = false;
            if (null != await _applyService.PostApply(Apply))
                await _navigation.PopModalAsync();
        }
    }
}
