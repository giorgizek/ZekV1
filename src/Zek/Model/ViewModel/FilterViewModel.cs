using Zek.PagedList;

namespace Zek.Model.ViewModel
{
    public interface IFilterViewModel<T>
    {
        T Default { get; }

        int Page { get; set; }
        int PageSize { get; set; }
        int PageCount { get; }
        int FirstItemOnPage { get; }
        int LastItemOnPage { get; }
        int ItemsCount { get; }
        IPagedList<T> PagedList { get; set; }
    }

    public class FilterViewModel<T> : IFilterViewModel<T>
    {
        public FilterViewModel()
        {
            Default = default(T);
            Page = 1;
            PageSize = 10;
        }

        public virtual void SetPage(int? page = null, int? pageSize = null)
        {
            Page = page.GetValueOrDefault(1);
            PageSize = pageSize.GetValueOrDefault(10);
        }

        public T Default { get; }

        public int Page { get; set; }

        private int _pageSize;

        public int PageSize
        {
            get => _pageSize;
            set
            {
                if (value < 1)
                    value = 1;
                else if (value > MaxPageSize)
                    value = MaxPageSize;

                _pageSize = value;
            }
        }

        public int PageCount => PagedList?.PageCount ?? 0;

        public int FirstItemOnPage => PagedList?.FirstItemOnPage ?? 0;

        public int LastItemOnPage => PagedList?.LastItemOnPage ?? 0;

        public int ItemsCount => PagedList?.TotalItemCount ?? 0;

        protected virtual int MaxPageSize => 500;

        public IPagedList<T> PagedList { get; set; }
    }
}
