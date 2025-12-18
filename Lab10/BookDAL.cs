using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10
{
    public class BookDAL
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Lab9_AdvancedSQL; Integrated Security=True; Encrypt=False; TrustServerCertificate=True;";


        // GET ALL BOOKS
        public List<Book> GetAllBooks()
        {
            List<Book> books = new List<Book>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT BookID, Title, AuthorID AS AuthorName, Price, Quantity, PublicationYear FROM Books";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        books.Add(new Book
                        {
                            BookID = (int)reader["BookID"],
                            Title = reader["Title"].ToString(),
                            AuthorName = reader["AuthorName"].ToString(),
                            PublicationYear = (int)reader["PublicationYear"],
                            Price = (decimal)reader["Price"],
                            Quantity = (int)reader["Quantity"]
                        });
                    }
                }
            }
            return books;
        }

        // INSERT BOOK
        public void InsertBook(Book book)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Books (BookID, Title, AuthorID, Price, Quantity, PublicationYear) " +
                              "VALUES (@BookID, @Title, @AuthorID, @Price, @Quantity, @Year)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@BookID", book.BookID);
                    cmd.Parameters.AddWithValue("@Title", book.Title);
                    cmd.Parameters.AddWithValue("@AuthorID", int.Parse(book.AuthorName));
                    cmd.Parameters.AddWithValue("@Price", book.Price);
                    cmd.Parameters.AddWithValue("@Quantity", book.Quantity);
                    cmd.Parameters.AddWithValue("@Year", book.PublicationYear);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // UPDATE BOOK
        public void UpdateBook(Book book)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Books SET Title=@Title, AuthorID=@AuthorID, " +
                              "Price=@Price, Quantity=@Quantity, PublicationYear=@Year " +
                              "WHERE BookID=@BookID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@BookID", book.BookID);
                    cmd.Parameters.AddWithValue("@Title", book.Title);
                    cmd.Parameters.AddWithValue("@AuthorID", int.Parse(book.AuthorName));
                    cmd.Parameters.AddWithValue("@Price", book.Price);
                    cmd.Parameters.AddWithValue("@Quantity", book.Quantity);
                    cmd.Parameters.AddWithValue("@Year", book.PublicationYear);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // DELETE BOOK
        public void DeleteBook(int bookId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Books WHERE BookID=@BookID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@BookID", bookId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
