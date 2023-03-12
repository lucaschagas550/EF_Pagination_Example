using EF_Pagination_Example.Data.Pagination.Base;
using EF_Pagination_Example.Data.Pagination.Page;
using EF_Pagination_Example.Data.Repositories.Base;
using EF_Pagination_Example.Data.Repositories.Interfaces;
using EF_Pagination_Example.Data.Uow.Interfaces;
using EF_Pagination_Example.Model;
using Microsoft.EntityFrameworkCore;

namespace EF_Pagination_Example.Data.Repositories.DataAccess
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(IUnitOfWork uow, AppDbContext context) : base(uow, context)
        {
        }

        public async Task<Page<Category>> Get(CategoryPage categoryPage)
        {
            IQueryable<Category> queryData = _context.Category.AsQueryable();
            ListApplyWhere(categoryPage, ref queryData);
            ListApplyOrderBy(categoryPage, ref queryData);

            var content = await Paginate(queryData, categoryPage).ConfigureAwait(false);
            var total = await queryData.CountAsync().ConfigureAwait(false);

            return new Page<Category>(total, content, categoryPage);
        }

        private void ListApplyWhere(CategoryPage categoryPage, ref IQueryable<Category> queryData)
        {
            if (!string.IsNullOrWhiteSpace(categoryPage.Search)) queryData = queryData.Where(c => c.Name.ToUpper().Contains(categoryPage.Search.ToUpper()));
            if (!string.IsNullOrWhiteSpace(categoryPage.Name)) queryData = queryData.Where(c => c.Name.ToUpper().Equals(categoryPage.Name.ToUpper()));
            if (!string.IsNullOrWhiteSpace(categoryPage.Description)) queryData = queryData.Where(o => o.Description.ToUpper().Equals(categoryPage.Description.ToUpper()));
        }

        private void ListApplyOrderBy(CategoryPage categoryPage, ref IQueryable<Category> queryData)
        {
            switch (categoryPage.Sort)
            {
                case nameof(Category.Name):
                    queryData = categoryPage.Direction.Equals(SortDirection.ASC) ? queryData.OrderBy(o => o.Name) : queryData.OrderByDescending(o => o.Name);
                    break;
                case nameof(Category.Description):
                    queryData = categoryPage.Direction.Equals(SortDirection.ASC) ? queryData.OrderBy(o => o.Description) : queryData.OrderByDescending(o => o.Description);
                    break;
                default:
                    queryData = queryData.OrderBy(o => o.Name);
                    break;
            }
        }
    }
}
