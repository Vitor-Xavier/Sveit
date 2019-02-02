using Sveit.Base.ViewModels;
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
        private readonly int _playerId;

        private readonly INavigation _navigation;

        private readonly IRequestService _requestService;

        private readonly IApplyService _applyService;

        public ObservableCollection<Apply> Applies { get; set; }

        public IAsyncCommand<Apply> ApplyCommand => new AsyncCommand<Apply>(ApplyCommandExecute); 

        public IAsyncCommand<Apply> UpdateApplyCommand => new AsyncCommand<Apply>(UpdateApplyCommandExecute);

        public AppliesPlayerViewModel(INavigation navigation, IRequestService requestService, int playerId)
        {
            _playerId = playerId;
            _navigation = navigation;
            _requestService = requestService;
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

        public async Task ApplyCommandExecute(Apply apply)
        {
            await _navigation.PushModalAsync(new Views.ApplyPage(_requestService, apply));
        }
        
        public async Task UpdateApplyCommandExecute(Apply apply)
        {
            await _navigation.PushModalAsync(new Views.ApplyRegisterPage(_requestService, apply.Vacancy, apply.ApplyId));
        }
    }
}
