using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms.MultiSelectListView;

namespace Sveit.ViewModels
{
    public class MultiSelectObservableGroupCollection<S, T> : MultiSelectObservableCollection<T>
    {
        private readonly S _key;
        public MultiSelectObservableGroupCollection(IGrouping<S, T> group)
            : base(group)
        {
            _key = group.Key;
        }
        public S Key
        {
            get { return _key; }
        }
    }
}
