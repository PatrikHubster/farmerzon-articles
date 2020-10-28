using System;
using System.ComponentModel.DataAnnotations;

namespace FarmerzonArticlesDataTransferModel
{
    public class ArticleInput
    {
        [Required]
        public UnitInput Unit { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public double Size { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }
    }
}