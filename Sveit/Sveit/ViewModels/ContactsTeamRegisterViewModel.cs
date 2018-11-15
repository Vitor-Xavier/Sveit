using Sveit.Controls;
using Sveit.Extensions;
using Sveit.Models;
using Sveit.Services.ContactType;
using Sveit.Services.Requests;
using Sveit.Services.Team;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sveit.ViewModels
{
    public class ContactsTeamRegisterViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;

        private readonly IContactTypeService _contactTypeService;

        private readonly ITeamService _teamService;

        private string contactText;

        public string ContactText
        {
            get { return contactText; }
            set { contactText = value; OnPropertyChanged(); }
        }

        private ContactType contactType;

        public ContactType ContactType
        {
            get { return contactType; }
            set { contactType = value; OnPropertyChanged(); }
        }

        private Team team;

        public Team Team
        {
            get { return team; }
            set { team = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Contact> Contacts { get; }

        public ObservableCollection<ContactType> ContactTypes { get; }

        public IAsyncCommand AddContactCommand => new AsyncCommand(AddContactCommandExecute);

        public IAsyncCommand FinalizeCommand => new AsyncCommand(FinalizeCommandExecute);

        public ContactsTeamRegisterViewModel(INavigation navigation, IRequestService requestService, Team team)
        {
            _navigation = navigation;
            Team = team;
            if (AppSettings.ApiStatus)
            {
                _contactTypeService = new ContactTypeService(requestService);
                _teamService = new TeamService(requestService);
            }
            else
            {
                _contactTypeService = new FakeContactTypeService();
                _teamService = new FakeTeamService();
            }
            Contacts = new ObservableCollection<Contact>();
            ContactTypes = new ObservableCollection<ContactType>();

            Task.Run(async () =>
            {
                await LoadContactTypes();
                await LoadContacts();
            });
        }

        public async Task LoadContactTypes()
        {
            var contactTypes = await _contactTypeService.GetContactTypesAsync();

            ContactTypes.Clear();
            foreach (ContactType c in contactTypes)
                ContactTypes.Add(c);
        }

        private async Task LoadContacts()
        {
            var contacts = await _teamService.GetTeamContacts(Team.TeamId);

            Contacts.Clear();
            foreach (Contact c in contacts)
                Contacts.Add(c);
        }

        private async Task AddContactCommandExecute()
        {
            if (!AddContactCommandCanExecute())
            {
                DependencyService.Get<IMessage>().ShortAlert(AppResources.InvalidValues);
                return;
            }
            var contact = new Models.Contact
            {
                Text = ContactText,
                ContactTypeId = ContactType.ContactTypeId
            };

            await _teamService.PostTeamContact(Team.TeamId, contact);
            await LoadContacts();
        }

        private bool AddContactCommandCanExecute()
        {
            if (string.IsNullOrWhiteSpace(ContactText) || ContactText.Length < 4)
                return false;
            return true;
        }

        private async Task FinalizeCommandExecute()
        {
            await _navigation.PopToRootAsync();
        }
    }
}
