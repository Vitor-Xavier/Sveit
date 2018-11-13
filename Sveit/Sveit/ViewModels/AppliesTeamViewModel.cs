using Sveit.Extensions;
using Sveit.Models;
using Sveit.Services.Apply;
using Sveit.Services.Requests;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sveit.ViewModels
{
    public class AppliesTeamViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;

        private readonly IApplyService _applyService;

        private Vacancy vacancy;

        public Vacancy Vacancy
        {
            get { return vacancy; }
            set { vacancy = value; OnPropertyChanged(); }
        }


        public ObservableCollection<Apply> Applies { get; set; }

        public IAsyncCommand<Apply> ApplySelectedCommand => new AsyncCommand<Apply>(ApplySelectedCommandExecute);

        public AppliesTeamViewModel(INavigation navigation, Vacancy vacancy)
        {
            _navigation = navigation;
            Vacancy = vacancy;
            if (AppSettings.ApiStatus)
                _applyService = new ApplyService(new RequestService());
            else
                _applyService = new FakeApplyService();
            Applies = new ObservableCollection<Apply>();

            Task.Run(() => LoadApplies());
        }

        private async Task LoadApplies()
        {
            if (IsLoading) return;
            IsLoading = true;

            var applies = await _applyService.GetAppliesByVacancy(Vacancy.VacancyId);

            Applies.Clear();
            foreach (Apply a in applies)
                Applies.Add(a);
            IsLoading = false;
        }

        public async Task ApplySelectedCommandExecute(Models.Apply apply)
        {
            await _navigation.PushModalAsync(new Views.ApplyPage(apply, true));
        }
    }
}
