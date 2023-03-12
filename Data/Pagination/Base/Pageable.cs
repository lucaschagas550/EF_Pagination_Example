﻿namespace EF_Pagination_Example.Data.Pagination.Base
{
    public class Pageable
    {
        private static readonly int MAX_SIZE = 500;

        private int _page = 1;
        private int _size = 10;

        public int Page
        {
            get => _page;
            set => _page = value > 0 ? value : _page;
        }

        public int Size
        {
            get => _size;
            set => _size = value > 0 && value <= MAX_SIZE ? value : _size;
        }

        public string? Search { get; set; }
        public string? Sort { get; set; }
        public SortDirection? Direction { get; set; } = SortDirection.ASC;
    }

    public enum SortDirection
    {
        ASC,
        DESC
    }
}