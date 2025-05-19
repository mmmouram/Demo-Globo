namespace MyApp.Models
{
    public class Client
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string CNPJ { get; set; } // Read-only in details view
        public string CorporateName { get; set; } // Read-only in details view
        // Additional client properties...
    }
}
