using Sveit.Extensions;
using Sveit.Models;
using Sveit.Services.Apply;
using Sveit.Services.Requests;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sveit.ViewModels
{
    public class AppliesPlayerViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;

        private readonly int _playerId;

        private readonly IApplyService _applyService;

        public ObservableCollection<Apply> Applies { get; set; }

        public IAsyncCommand<Apply> ApplySelectedCommand => new AsyncCommand<Apply>(ApplySelectedCommandExecute);

        public AppliesPlayerViewModel(INavigation navigation, int playerId)
        {
            _navigation = navigation;
            _playerId = playerId;
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

            var applies = await _applyService.GetAppliesByPlayer(_playerId);

            Applies.Clear();
            foreach (Apply a in applies)
                Applies.Add(a);
            IsLoading = false;
        }

        public async Task ApplySelectedCommandExecute(Apply apply)
        {
            await _navigation.PushModalAsync(new Views.ApplyPage(apply));
        }
    }
}
