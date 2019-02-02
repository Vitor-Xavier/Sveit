using Sveit.Base.ViewModels;
using Sveit.Controls;
using Sveit.Extensions;
using Sveit.Models;
using Sveit.Services.Apply;
using Sveit.Services.Requests;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sveit.ViewModels
{
    public class ApplyRegisterViewModel : BaseViewModel
    {
        private readonly int _applyId;

        private readonly IApplyService _applyService;

        private readonly INavigation _navigation;

        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; OnPropertyChanged(); }
        }

        private Vacancy vacancy;

        public Vacancy Vacancy
        {
            get { return vacancy; }
            set { vacancy = value; OnPropertyChanged(); }
        }

        public List<MultiSelectObservableGroupCollection<RoleType, Role>> RoleTypes { get; set; }

        public IAsyncCommand ApplyCommand => new AsyncCommand(ApplyCommandExecute);

        public ApplyRegisterViewModel(INavigation navigation, IRequestService requestService, Vacancy vacancy, int applyId)
        {
            _navigation = navigation;
            _applyId = applyId;
            if (AppSettings.ApiStatus)
                _applyService = new ApplyService(requestService);
            else
                _applyService = new FakeApplyService();

            Vacancy = vacancy;
            RoleTypes = vacancy.Roles.OrderBy(p => p.Name)
               .GroupBy(p => p.RoleType)
               .Select(p => new MultiSelectObservableGroupCollection<RoleType, Role>(p)).ToList();

            Task.Run(() => LoadApply());
        }

        private async Task ApplyCommandExecute()
        {
            if (!ApplyCommandCanExecute())
            {
                DependencyService.Get<IMessage>().ShortAlert(AppResources.InvalidValues);
                return;
            }

            var roles = new List<Role>();
            foreach (var group in RoleTypes)
                roles.AddRange(group.SelectedItems);

            var apply = new Apply
            {
                ApplyId = _applyId,
                Text = Text,
                PlayerId = App.LoggedPlayer.PlayerId,
                VacancyId = Vacancy.VacancyId,
                Roles = roles,
                Approved = null
            };

            var result = await _applyService.PostApply(apply);
            if (result != null)
                await _navigation.PopModalAsync();
            else
                DependencyService.Get<IMessage>().ShortAlert(AppResources.ApplyFailed);
        }

        private bool ApplyCommandCanExecute()
        {
            if (string.IsNullOrWhiteSpace(Text))
                return false;
            return true;
        }

        private async Task LoadApply()
        {
            if (_applyId == 0) return;

            var apply = await _applyService.GetApply(_applyId);

            Text = apply.Text;
            Vacancy = apply.Vacancy;
            RoleTypes = apply.Vacancy.Roles.OrderBy(p => p.Name)
               .GroupBy(p => p.RoleType)
               .Select(p => new MultiSelectObservableGroupCollection<RoleType, Role>(p)).ToList();
        }
    }
}
