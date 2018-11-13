using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Sveit.Extensions;
using Sveit.Models;
using Sveit.Services.Requests;
using Sveit.Services.Vacancy;
using Xamarin.Forms;

namespace Sveit.ViewModels
{
    public class GameViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;

        private readonly IRequestService _requestService;

        private readonly IVacancyService _vacancyService;

        private Game game;

        public Game Game
        {
            get { return game; }
            set { game = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Vacancy> Vacancies { get; }

        public IAsyncCommand<Vacancy> VacancyCommand => new AsyncCommand<Vacancy>(VacancyCommandExecute);

        public GameViewModel(INavigation navigation, IRequestService requestService, Game game)
        {
            _navigation = navigation;
            _requestService = requestService;
            if (AppSettings.ApiStatus)
                _vacancyService = new VacancyService(requestService);            
            else 
                _vacancyService = new FakeVacancyService();
                
            Game = game;

            Vacancies = new ObservableCollection<Vacancy>();
            Task.Run(() => LoadVacancies());
        }

        private async Task LoadVacancies()
        {
            if (IsLoading) return;
            IsLoading = true;
            var vacancies = await _vacancyService.GetVacanciesByGameAsync(Game.GameId);

            Vacancies.Clear();
            foreach (Vacancy v in vacancies)
                Vacancies.Add(v);
            IsLoading = false;
        }

        public async Task VacancyCommandExecute(Vacancy vacancy)
        {
            await _navigation.PushModalAsync(new Views.VacancyPage(_requestService, vacancy));
        }
    }
}
