using Sveit.Models;
using Sveit.Services.ContactType;
using Sveit.Services.Player;
using Sveit.Services.Requests;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sveit.ViewModels
{
    public class ContactsPlayerRegisterViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;

        private readonly IContactTypeService _contactTypeService;

        private readonly IPlayerService _playerService;

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

        private Player player;

        public Player Player
        {
            get { return player; }
            set { player = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Contact> Contacts { get; }

        public ObservableCollection<ContactType> ContactTypes { get; }

        public ICommand AddContactCommand => new Command(AddContactCommandExecute);

        public ContactsPlayerRegisterViewModel(INavigation navigation, Player player)
        {
            _navigation = navigation;
            Player = player;
            if (AppSettings.ApiStatus)
            {
                IRequestService requestService = new RequestService();
                _contactTypeService = new ContactTypeService(requestService);
                _playerService = new PlayerService(requestService);
            }
            else
            {
                _contactTypeService = new FakeContactTypeService();
                _playerService = new FakePlayerService();
            }
            Contacts = new ObservableCollection<Contact>();
            ContactTypes = new ObservableCollection<ContactType>();
            LoadContacts();
            LoadContactTypes();
        }

        private async void LoadContacts()
        {
            var contacts = await _playerService.GetPlayerContacts(Player.PlayerId);

            Contacts.Clear();
            foreach (Contact c in contacts)
                Contacts.Add(c);
        }

        public async void LoadContactTypes()
        {
            var contactTypes = await _contactTypeService.GetContactTypesAsync();

            ContactTypes.Clear();
            foreach (ContactType c in contactTypes)
                ContactTypes.Add(c);
        }

        private async void AddContactCommandExecute()
        {
            if (ContactType == null) return;
            var contact = new Models.Contact
            {
                Text = ContactText,
                ContactTypeId = ContactType.ContactTypeId
            };

            await _playerService.PostPlayerContact(Player.PlayerId, contact);
            LoadContacts();
        }
    }
}
