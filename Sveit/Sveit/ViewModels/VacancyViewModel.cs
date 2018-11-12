using Sveit.Models;
using Sveit.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sveit.ViewModels
{
    public class VacancyViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;

        private Vacancy vacancy;

        public Vacancy Vacancy
        {
            get { return vacancy; }
            set { vacancy = value; OnPropertyChanged(); }
        }

        public ICommand ApplyCommand => new Command(ExecuteApplyCommand);

        public ObservableCollection<Player> Members { get; set; }

        public VacancyViewModel(INavigation navigation, Vacancy vacancy)
        {
            Vacancy = vacancy;
            _navigation = navigation;
        }

        public async void ExecuteApplyCommand()
        {
            await _navigation.PushModalAsync(new ApplyRegisterPage(Vacancy));
        }
    }
}
