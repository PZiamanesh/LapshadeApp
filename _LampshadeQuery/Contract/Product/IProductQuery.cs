namespace _LampshadeQuery.Contract.Product;

public interface IProductQuery
{
    IEnumerable<ProductQueryViewModel> GetLatestProducts();
    IEnumerable<ProductQueryViewModel> Search(string searchKey);
}
