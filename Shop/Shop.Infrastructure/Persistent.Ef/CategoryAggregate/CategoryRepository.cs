using Microsoft.EntityFrameworkCore;
using Shop.Domain.CategoryAggregate;
using Shop.Domain.CategoryAggregate.Repository;
using Shop.Infrastructure.Persistent.Ef._Utilities;

namespace Shop.Infrastructure.Persistent.Ef.CategoryAggregate
{
    internal class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ShopContext context) : base(context)
        {
        }

        public async Task<bool> DeleteCategory(long categoryId)
        {
            var category = await Context.Categories
                            .Include(c => c.Childs)
                            .ThenInclude(c => c.Childs).FirstOrDefaultAsync(c => c.Id == categoryId);

            if (category == null)
                return false;

       
            var isExistProduct = await Context.Products.AnyAsync(p => p.CategoryId == categoryId ||
                                    p.SubCategoryId == categoryId ||
                                    p.SecondarySubCategoryId == categoryId);

            if (isExistProduct)
                return false;

         
            if (category.Childs.Any(c => c.Childs.Any()))
            {
                Context.RemoveRange(category.Childs.SelectMany(c => c.Childs));
            }

            Context.RemoveRange(category.Childs);

            Context.RemoveRange(category);

            return true;
        }
    }
}
