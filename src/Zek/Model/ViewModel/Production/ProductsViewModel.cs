using System.ComponentModel.DataAnnotations;
using Zek.Localization;

namespace Zek.Model.ViewModel.Production
{
    public class ProductsViewModel : ListBaseViewModel
    {
        [Display(Name = nameof(ApplicationResources.Name), ResourceType = typeof(ApplicationResources))]
        public string Name { get; set; }

        [Display(Name = nameof(ProductResources.ProductNumber), ResourceType = typeof(ProductResources))]
        public string ProductNumber { get; set; }

        [Display(Name = nameof(ProductResources.Category), ResourceType = typeof(ProductResources))]
        public int? CategoryId { get; set; }
        [Display(Name = nameof(ProductResources.Category), ResourceType = typeof(ProductResources))]
        public string Category { get; set; }

        [Display(Name = nameof(ProductResources.SubCategory), ResourceType = typeof(ProductResources))]
        public int? SubCategoryId { get; set; }

        [Display(Name = nameof(ProductResources.SubCategory), ResourceType = typeof(ProductResources))]
        public string SubCategory { get; set; }
    }
}
