using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Zek.Localization;

namespace Zek.Model.ViewModel.Production
{
    public class ProductFilterViewModel : FilterViewModel<ProductsViewModel>
    {
        [Display(Name = nameof(ApplicationResources.Id), ResourceType = typeof(ApplicationResources))]
        public int? Id { get; set; }

        [Display(Name = nameof(ApplicationResources.Name), ResourceType = typeof(ApplicationResources))]
        public string Name { get; set; }

        [Display(Name = nameof(ProductResources.ProductNumber), ResourceType = typeof(ProductResources))]
        public string ProductNumber { get; set; }


        [Display(Name = nameof(ProductResources.Category), ResourceType = typeof(ProductResources))]
        public int? CategoryId { get; set; }

        [BindNever]
        public SelectList Categories { get; set; }

        [Display(Name = nameof(ProductResources.SubCategory), ResourceType = typeof(ProductResources))]
        public int? SubCategoryId { get; set; }

        [BindNever]
        public SelectList SubCategories { get; set; }
    }
}
