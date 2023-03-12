using Microsoft.AspNetCore.Http.HttpResults;

namespace EF_Pagination_Example.Data.Pagination.Base
{
    public class Page<T>
    {
        private const int GREATER_THAN_ONE = 1;
        private const int EQUAL_TO_ONE = 1;

        public IEnumerable<T> Content { get; }
        public int TotalPages { get; }
        public int TotalElements { get; }
        public int Number { get; }
        public int Size { get; }
        public bool HasPrevious { get; }
        public bool HasNext { get; }
        public bool IsFirst { get; }
        public bool IsLast { get; }

        public Page(int total, IEnumerable<T> content, Pageable pageable)
        {
            Number = pageable.Page;
            Size = pageable.Size;

            Content = content;

            TotalElements = total;

            TotalPages = (int)Math.Ceiling((double)TotalElements / pageable.Size);

            HasPrevious = Number > GREATER_THAN_ONE;
            HasNext = Number < TotalPages;

            IsFirst = Number == EQUAL_TO_ONE;
            IsLast = Number == TotalPages;
        }
    }
}
