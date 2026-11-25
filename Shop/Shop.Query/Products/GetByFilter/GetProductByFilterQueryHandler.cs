using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Products.DTOs;

namespace Shop.Query.Products.GetByFilter
{
    public class GetProductByFilterQueryHandler : IQueryHandler<GetProductByFilterQuery, ProductFilterResult>
    {
        private readonly ShopContext _shopContext;

        public GetProductByFilterQueryHandler(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<ProductFilterResult> Handle(GetProductByFilterQuery request, CancellationToken cancellationToken)
        {
            var @params = request.FilterParams;
            var result = _shopContext.Products.OrderByDescending(p => p.Id).AsQueryable();

            if (@params.Id != null)
                result = result.Where(p => p.Id == @params.Id);

            if (!string.IsNullOrWhiteSpace(@params.Slug))
                result = result.Where(p => p.Slug == @params.Slug);

            if (!string.IsNullOrWhiteSpace(@params.Title))
                result = result.Where(p => p.Title.Contains(@params.Title));

            var skip = (@params.PageId - 1) * @params.Take;
            var model = new ProductFilterResult()
            {
                Data = await result.Skip(skip).Take(@params.Take).Select(p => p.MapListData()).ToListAsync(cancellationToken),
                FilterParams = @params
            };
            model.GeneratePaging(result, @params.Take, @params.PageId);
            return model;
        }

    }
}
