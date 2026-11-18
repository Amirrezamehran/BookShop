using Common.Application;
using Shop.Application.Products.AddImage;
using Shop.Application.Products.Create;
using Shop.Application.Products.Edit;
using Shop.Application.Products.RemoveImage;
using Shop.Query.Products.DTOs;
using Shop.Query.Sellers.DTOs;

namespace Shop.Presentation.Facade.Products
{
    public interface IProductFacade
    {
        // Commands //
        Task<OperationResult> AddImageProduct(AddProductImageCommand command);
        Task<OperationResult> CreateProduct(CreateProductCommand command);
        Task<OperationResult> EditProduct(EditProductCommand command);
        Task<OperationResult> RemoveImageProduct(RemoveProductImageCommand command);


        // Queries //
        Task<ProductDto?> GetProductById(long id);
        Task<ProductDto?> GetProductBySlug(string slug);
        Task<ProductFilterResult> GetProductByFilter(ProductFilterParams filterParams);
        Task<ProductShopResult> GetProductForShop(ProductShopFilterParam filterParams);
        Task<SingleProductDto?> GetProductBySlugForSinglePage(string slug);

    }


    public class SingleProductDto
    {
        public ProductDto ProductDto { get; set; }
        public List<InventoryDto> Inventories { get; set; }
    }

}
