namespace _LampshadeQuery.Contract.Product;

public interface IProductQuery
{
    IEnumerable<ProductQueryViewModel> GetLatestProducts();
    ProductQueryViewModel GetProduct(string slug);
    IEnumerable<ProductQueryViewModel> Search(string searchKey);
}
