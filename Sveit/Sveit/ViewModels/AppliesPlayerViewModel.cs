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
        private readonly IApplyService _applyService;

        private int _playerId;

        public ObservableCollection<Apply> Applies { get; set; }

        public IAsyncCommand<Apply> ApplyCommand => new AsyncCommand<Apply>(ApplyCommandExecute); 

        public IAsyncCommand<Apply> UpdateApplyCommand => new AsyncCommand<Apply>(UpdateApplyCommandExecute);

        public AppliesPlayerViewModel(IApplyService applyService)
        {
            _applyService = applyService;

            Applies = new ObservableCollection<Apply>();
        }

        public override Task InitializeAsync(params object[] navigationData)
        {
            _playerId = navigationData[0] as int? ?? 0;
            return LoadApplies();
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
            await NavigationService.NavigateToAsync<ApplyViewModel>(apply);
        }
        
        public async Task UpdateApplyCommandExecute(Apply apply)
        {
            await NavigationService.NavigateToAsync<ApplyRegisterViewModel>(apply.Vacancy, apply.ApplyId);
        }
    }
}
