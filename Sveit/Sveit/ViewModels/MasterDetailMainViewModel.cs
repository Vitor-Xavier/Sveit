using Sveit.Base.ViewModels;
using System.Threading.Tasks;

namespace Sveit.ViewModels
{
    public class MasterDetailMainViewModel : BaseViewModel
    {
        MasterMainViewModel menuViewModel;

        public MasterMainViewModel MenuViewModel
        {
            get => menuViewModel;

            set
            {
                menuViewModel = value;
                OnPropertyChanged();
            }
        }

        public MasterDetailMainViewModel(MasterMainViewModel menuViewModel)
        {
            this.menuViewModel = menuViewModel;
        }

        public override Task InitializeAsync(params object[] navigationData) => 
            Task.WhenAll
                (

                    NavigationService.NavigateToAsync<HomeViewModel>()
                );
    }
}
