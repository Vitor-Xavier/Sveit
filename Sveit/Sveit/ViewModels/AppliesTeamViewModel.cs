using Sveit.Models;
using Sveit.Services.Apply;
using Sveit.Services.Requests;
using System.Collections.ObjectModel;
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

        public ICommand ApplySelectedCommand => new Command<Apply>(ApplySelectedCommandExecute);

        public AppliesTeamViewModel(INavigation navigation, Vacancy vacancy)
        {
            _navigation = navigation;
            Vacancy = vacancy;
            if (AppSettings.ApiStatus)
                _applyService = new ApplyService(new RequestService());
            else
                _applyService = new FakeApplyService();
            Applies = new ObservableCollection<Apply>();
            LoadApplies();
        }

        private async void LoadApplies()
        {
            if (IsLoading) return;
            IsLoading = true;

            var applies = await _applyService.GetAppliesByVacancy(Vacancy.VacancyId);

            Applies.Clear();
            foreach (Apply a in applies)
                Applies.Add(a);
            IsLoading = false;
        }

        public async void ApplySelectedCommandExecute(Models.Apply apply)
        {
            await _navigation.PushModalAsync(new Views.ApplyPage(apply, true));
        }
    }
}
