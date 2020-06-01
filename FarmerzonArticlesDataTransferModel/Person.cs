using System.Collections.Generic;

namespace FarmerzonArticlesDataTransferModel
{
    public class Person
    {
        public int PersonId { get; set; }
        
        public IList<Article> Articles { get; set; }
        
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
    }
}