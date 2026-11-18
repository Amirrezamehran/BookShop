using Microsoft.EntityFrameworkCore;
using Shop.Domain.ProductAggregate;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Products.DTOs;

namespace Shop.Query.Products
{
    public static class ProductMapper
    {
        public static ProductDto? Map(this Product? product)
        {
            if (product == null)
                return null;

            return new ProductDto()
            {
                Id = product.Id,
                Title = product.Title,
                ImageName = product.ImageName,
                Description = product.Description,
                Slug = product.Slug,
                SeoData = product.SeoData,
                Images = product.Images.Select(p => new ProductImageDto()
                {
                    Id = p.Id,
                    ProductId = p.ProductId,
                    ImageName = p.ImageName,
                    Sequence = p.Sequence,
                    CreationDate = p.CreationDate

                }).ToList(),
                Specifications = product.Specifications.Select(p => new ProductSpecificationDto()
                {
                    Key = p.Key,
                    Value = p.Value

                }).ToList(),

        
                Category = new ProductCategoryDto() { Id = product.CategoryId },
                SubCategory = new ProductCategoryDto() { Id = product.SubCategoryId },
                SecondarySubCategory = product.SecondarySubCategoryId != null ?
                                    new ProductCategoryDto() 
                                    {
                                        Id = (long)product.SecondarySubCategoryId
                                    } : null
            };
        }


        public static ProductFilterData MapListData(this Product product)
        {
            return new ProductFilterData()
            {
                Id = product.Id,
                Title = product.Title,
                ImageName = product.ImageName,
                Slug = product.Slug,
                CreationDate = product.CreationDate
            };
        }


        public static async Task SetCategories(this ProductDto productDto, ShopContext Context)
        {
            var categories = await Context.Categories.Where(c => c.Id == productDto.Category.Id || c.Id == productDto.SubCategory.Id)
                    .Select(p => new ProductCategoryDto()
                    {
                        Id = p.Id,
                        ParentId = p.ParentId,
                        Title = p.Title,
                        Slug= p.Slug,
                        SeoData = p.SeoData

                    }).ToListAsync();



            if (productDto.SecondarySubCategory != null)
            {
                var secondarySubCategory = await Context.Categories.Where(c => c.Id == productDto.SecondarySubCategory.Id)
                    .Select(p => new ProductCategoryDto()
                    {
                        Id = p.Id,
                        ParentId = p.ParentId,
                        Title = p.Title,
                        Slug = p.Slug,
                        SeoData = p.SeoData

                    }).FirstOrDefaultAsync();

                if (secondarySubCategory != null)
                    productDto.SecondarySubCategory = secondarySubCategory;
            }


            if (categories != null)
                productDto.Category = categories.First(c => c.Id == productDto.Category.Id);

            if (categories != null)
                productDto.SubCategory = categories.First(c => c.Id == productDto.SubCategory.Id);

        }


    }

}
