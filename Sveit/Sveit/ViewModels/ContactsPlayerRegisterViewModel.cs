using Rg.Plugins.Popup.Extensions;
using Sveit.Controls;
using Sveit.Extensions;
using Sveit.Models;
using Sveit.Services.ContactType;
using Sveit.Services.Player;
using Sveit.Services.Requests;
using Sveit.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sveit.ViewModels
{
    public class ContactsPlayerRegisterViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;

        private readonly IRequestService _requestService;

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

        private Player _player;

        public Player Player
        {
            get { return _player; }
            set { _player = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Contact> Contacts { get; }

        public ObservableCollection<ContactType> ContactTypes { get; }

        public IAsyncCommand AddContactCommand => new AsyncCommand(AddContactCommandExecute);

        public IAsyncCommand ContactTypeCommand => new AsyncCommand(ContactTypeCommandExecute);

        public ICommand FinalizeCommand => new Command(FinalizeCommandExecute);

        public ContactsPlayerRegisterViewModel(INavigation navigation, IRequestService requestService, Player player)
        {
            _navigation = navigation;
            _requestService = requestService;
            Player = player;
            if (AppSettings.ApiStatus)
            {
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

            Task.Run(async () =>
            {
                await LoadContactTypes();
                await LoadContacts();
            });
        }

        private async Task LoadContacts()
        {
            var contacts = await _playerService.GetPlayerContacts(Player.PlayerId);

            Contacts.Clear();
            foreach (Contact c in contacts)
                Contacts.Add(c);
        }

        public async Task LoadContactTypes()
        {
            var contactTypes = await _contactTypeService.GetContactTypesAsync();

            ContactTypes.Clear();
            foreach (ContactType c in contactTypes)
                ContactTypes.Add(c);
            ContactType = ContactTypes.FirstOrDefault();
        }

        private async Task AddContactCommandExecute()
        {
            if (!AddContactCommandCanExecute())
            {
                DependencyService.Get<IMessage>().ShortAlert(AppResources.InvalidValues);
                return;
            }
            var contact = new Contact
            {
                Text = ContactText,
                ContactTypeId = ContactType.ContactTypeId
            };

            await _playerService.PostPlayerContact(Player.PlayerId, contact);
            await LoadContacts();
        }

        private bool AddContactCommandCanExecute()
        {
            if (string.IsNullOrWhiteSpace(ContactText) || ContactText.Length < 4)
                return false;
            return true;
        }

        private void FinalizeCommandExecute()
        {
            App.Current.MainPage = new Views.MasterMainPage(_requestService);
        }

        private async Task ContactTypeCommandExecute()
        {
            var popupContactType = new PopupPickerPage
            {
                BindingContext = new PopupPickerViewModel<ContactType>(_navigation, ContactTypes)
            };
            (popupContactType.BindingContext as PopupPickerViewModel<ContactType>).ItemSelected += HandleContactTypeSelected;
            (popupContactType.BindingContext as PopupPickerViewModel<ContactType>).Title = AppResources.ContactType;
            await _navigation.PushPopupAsync(popupContactType);
        }

        private async void HandleContactTypeSelected(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var item = e.Item as ContactType;
            ContactType = item;
            await _navigation.PopAllPopupAsync();
        }
    }
}
