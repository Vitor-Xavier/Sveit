using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Sveit.Models;
using Sveit.Services.Requests;
using Sveit.Services.Vacancy;
using Xamarin.Forms;

namespace Sveit.ViewModels
{
    public class GameViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;

        private readonly IVacancyService _vacancyService;

        public Game Game { get; set; }

        public ObservableCollection<Vacancy> Vacancies { get; set; }

        public GameViewModel(INavigation navigation, Game game)
        {
            _navigation = navigation;
            if (AppSettings.ApiStatus)
                _vacancyService = new VacancyService(new RequestService());
            else
                _vacancyService = new FakeVacancyService();


            Vacancies = new ObservableCollection<Vacancy>();
            Game = game;
            LoadVacancies();
        }

        private async void LoadVacancies()
        {
            if (IsLoading) return;
            IsLoading = true;
            var vacancies = await _vacancyService.GetVacanciesByGameAsync(Game.GameId);

            Vacancies.Clear();
            foreach (Vacancy v in vacancies)
                Vacancies.Add(v);
            IsLoading = false;
        }

        public async void VacancySelectedCommandExecute(Vacancy vacancy)
        {
            await _navigation.PushModalAsync(new Views.VacancyPage(vacancy));
        }
    }
}
