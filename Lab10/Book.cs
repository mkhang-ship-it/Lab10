using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10
{
    public class Book
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Lab9_AdvancedSQL; Integrated Security=True; Encrypt=True; TrustServerCertificate=True;";
        public int BookID { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public int PublicationYear { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int? GenreID { get; set; }  // Nullable
    }
}
