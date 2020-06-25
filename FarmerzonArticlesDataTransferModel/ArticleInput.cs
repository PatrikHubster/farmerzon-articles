using System;
using System.ComponentModel.DataAnnotations;

namespace FarmerzonArticlesDataTransferModel
{
    public class ArticleInput
    {
        [Required(ErrorMessage = "The unit of the article is missing.")]
        public UnitInput Unit { get; set; }
        
        [Required(ErrorMessage = "The name of the article is missing.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The description of the article is missing.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "The price of the article is missing.")]
        public double? Price { get; set; }
        [Required(ErrorMessage = "The amount of the article is missing.")]
        public int? Amount { get; set; }
        [Required(ErrorMessage = "The size of the article is missing.")]
        public double? Size { get; set; }
        [Required(ErrorMessage = "The expiration date of the article is missing.")]
        public DateTime ExpirationDate { get; set; }
    }
}