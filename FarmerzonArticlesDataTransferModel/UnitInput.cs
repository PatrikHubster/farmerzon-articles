using System.ComponentModel.DataAnnotations;

namespace FarmerzonArticlesDataTransferModel
{
    public class UnitInput
    {
        [Required]
        public string Name { get; set; }
    }
}