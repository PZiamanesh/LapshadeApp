using _LampshadeQuery.Contract.ArticleCategory;
using _LampshadeQuery.Contract.ProductCategory;

namespace _LampshadeQuery.Contract.Menu;

public class MenuQueryModel
{
    public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }
    public List<ProductCategoryQueryModel> ProductCategories { get; set; }
}
