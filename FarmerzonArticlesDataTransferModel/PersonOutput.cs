namespace FarmerzonArticlesDataTransferModel
{
    public class PersonOutput : BaseModelOutput
    {
        public long PersonId { get; set; }

        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
    }
}