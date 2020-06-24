using System.ComponentModel.DataAnnotations;

namespace FarmerzonArticlesDataTransferModel
{
    public class UnitInput
    {
        [Required(ErrorMessage = "The name of the unit is missing.")]
        public string Name { get; set; }
    }
}