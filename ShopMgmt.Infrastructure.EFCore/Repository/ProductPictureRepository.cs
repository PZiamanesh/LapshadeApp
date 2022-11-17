using _Framework.Application;
using _Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopMgmt.Application.Contract.ProductPicture;
using ShopMgmt.Domain.ProductPictureAggr;

namespace ShopMgmt.Infrastructure.EFCore.Repository;
#nullable disable

public class ProductPictureRepository : RepositoryBase<long, ProductPicture>, IProductPictureRepository
{
    private readonly ShopContext _context;

    public ProductPictureRepository(ShopContext context) : base(context)
    {
        _context = context;
    }

    public EditProductPicture GetDetails(long id)
    {
        return _context.ProductPictures
            .Select(x => new EditProductPicture()
            {
                Id = x.Id,
                ProductId = x.ProductId,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle
            }).FirstOrDefault(x => x.Id == id);
    }

    public IEnumerable<ProductPictureViewModel> Search(ProductPictureSearchViewModel searchModel)
    {
        var query = _context.ProductPictures
            .Include(x => x.Product)
            .Select(x => new ProductPictureViewModel()
            {
                Id = x.Id,
                Product = x.Product.Name,
                Picture = x.Picture,
                CreationDate = x.CreationDate.ToFarsi(),
                ProductId = x.ProductId,
                IsRemoved = x.IsRemoved
            });

        if (searchModel.ProductId > 0)
        {
            query = query.Where(x => x.ProductId == searchModel.ProductId);
        }

        return query.OrderByDescending(x=>x.Id).ToList();
    }
}
