namespace Zek.Model.ViewModel
{
    public class PopupFilterViewModel<T> : FilterViewModel<T>
    {
        public PopupFilterViewModel()
        {
            PageSize = 10;
        }
        protected override int MaxPageSize => 10;
    }
}