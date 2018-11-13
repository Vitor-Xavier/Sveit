using Sveit.Extensions;
using Sveit.Models;
using Sveit.Services.Apply;
using Sveit.Services.Requests;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sveit.ViewModels
{
    public class ApplyRegisterViewModel : BaseViewModel
    {
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

        public ApplyRegisterViewModel(INavigation navigation, IRequestService requestService, Vacancy vacancy)
        {
            _navigation = navigation;
            if (AppSettings.ApiStatus)
                _applyService = new ApplyService(requestService);
            else
                _applyService = new FakeApplyService();

            Vacancy = vacancy;
            RoleTypes = vacancy.Roles.OrderBy(p => p.Name)
               .GroupBy(p => p.RoleType)
               .Select(p => new MultiSelectObservableGroupCollection<RoleType, Role>(p)).ToList();
        }

        private async Task ApplyCommandExecute()
        {
            var roles = new List<Role>();
            foreach (var group in RoleTypes)
                roles.AddRange(group.SelectedItems);

            var apply = new Apply
            {
                Text = Text,
                PlayerId = App.LoggedPlayer.PlayerId,
                VacancyId = Vacancy.VacancyId,
                Roles = roles,
                Approved = null
            };

            var result = await _applyService.PostApply(apply);
            if (result != null)
                await _navigation.PopModalAsync();
        }
    }
}
