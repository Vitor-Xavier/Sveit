using Sveit.Extensions;
using Sveit.Models;
using Sveit.Services.Requests;
using Sveit.Views;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sveit.ViewModels
{
    public class VacancyViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;

        private readonly IRequestService _requestService;

        private Vacancy vacancy;

        public Vacancy Vacancy
        {
            get { return vacancy; }
            set { vacancy = value; OnPropertyChanged(); }
        }

        public IAsyncCommand ApplyCommand => new AsyncCommand(ExecuteApplyCommand);

        public ObservableCollection<Player> Members { get; set; }

        public VacancyViewModel(INavigation navigation, IRequestService requestService, Vacancy vacancy)
        {
            Vacancy = vacancy;
            _navigation = navigation;
            _requestService = requestService;
        }

        public async Task ExecuteApplyCommand()
        {
            await _navigation.PushModalAsync(new ApplyRegisterPage(_requestService, Vacancy));
        }
    }
}
