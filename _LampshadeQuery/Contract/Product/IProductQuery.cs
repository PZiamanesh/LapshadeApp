namespace _LampshadeQuery.Contract.Product;

public interface IProductQuery
{
    IEnumerable<ProductQueryModel> GetLatestProducts();
    ProductQueryModel GetProduct(string slug);
    IEnumerable<ProductQueryModel> Search(string searchKey);
}
