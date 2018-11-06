﻿using Sveit.Models;
using Sveit.Services.Requests;
using Sveit.Services.Vacancy;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sveit.ViewModels
{
    public class VacanciesTeamViewModel : BindableObject
    {
        private readonly int _teamId;

        private readonly INavigation _navigation;

        private readonly IVacancyService _vacancyService;

        public ObservableCollection<Vacancy> Vacancies { get; set; }

        public ICommand VacancySelectCommand => new Command<Vacancy>(VacancySelectCommandExecute);

        public VacanciesTeamViewModel(INavigation navigation, int teamId)
        {
            _navigation = navigation;
            _teamId = teamId;
            if (AppSettings.ApiStatus)
                _vacancyService = new VacancyService(new RequestService());
            else
                _vacancyService = new FakeVacancyService();
            Vacancies = new ObservableCollection<Vacancy>();
            LoadVacancies();
        }

        public async void LoadVacancies()
        {
            var vacancies = await _vacancyService.GetVacanciesByTeamAsync(_teamId);

            Vacancies.Clear();
            foreach (Vacancy v in vacancies)
                Vacancies.Add(v);
        }

        public async void VacancySelectCommandExecute(Vacancy vacancy)
        {
            await _navigation.PushAsync(new Views.AppliesTeamPage(vacancy));
        }
    }
}