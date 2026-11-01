using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Library
{
    public class Library
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=LibraryDB;Integrated Security=True;TrustServerCertificate=True;";

        // 1. ADD BOOK
        public void AddBook(int bookId, string title, string author, int quantity)
        {
            if (quantity < 0)
            {
                Console.WriteLine("\n Error: Copies cannot be negative.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Books (BookID, Title, Author, Quantity) VALUES (@id, @title, @author, @qty)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", bookId);
                        cmd.Parameters.AddWithValue("@title", title);
                        cmd.Parameters.AddWithValue("@author", author);
                        cmd.Parameters.AddWithValue("@qty", quantity);

                        cmd.ExecuteNonQuery();
                        Console.WriteLine("\n [SUCCESS] Book permanently saved to database!");
                    }
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627) // Primary Key violation
                    {
                        Console.WriteLine($"\n Error: Book ID {bookId} already exists.");
                    }
                    else
                    {
                        Console.WriteLine($"\n Database Error: {ex.Message}");
                    }
                }
            }
        }

        // 2. VIEW BOOKS
        public void ViewBooks()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Books";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("\n BOOKS IN DATABASE ");
                    if (!reader.HasRows)
                    {
                        Console.WriteLine("No books found in database.");
                        return;
                    }

                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["BookID"]} \n Title: {reader["Title"]} \n Author: {reader["Author"]} \n Copies: {reader["Quantity"]}");
                    }
                }
            }
        }

        // 3. SEARCH BOOK
        public void SearchBook(string queryText)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Books WHERE Title LIKE @q OR Author LIKE @q";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@q", "%" + queryText + "%");

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine($"\n SEARCH RESULTS FOR '{queryText}'");
                        if (!reader.HasRows)
                        {
                            Console.WriteLine("No matching books found.");
                            return;
                        }

                        while (reader.Read())
                        {
                            Console.WriteLine($"ID: {reader["BookID"]} \n Title: {reader["Title"]} \n Author: {reader["Author"]} \n Copies: {reader["Quantity"]}");
                        }
                    }
                }
            }
        }

        // 4. ADD USER
        public void AddUser(int userId, string name)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Users (UserID, Name) VALUES (@uid, @name)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@uid", userId);
                    cmd.Parameters.AddWithValue("@name", name);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("\n User added successfully to database!");
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"\n Database Error: {ex.Message}");
                    }
                }
            }
        }

        // 5. VIEW USERS
        public void ViewUsers()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Users";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("\n USERS LIST ");
                    if (!reader.HasRows)
                    {
                        Console.WriteLine("No users registered.");
                        return;
                    }

                    while (reader.Read())
                    {
                        Console.WriteLine($"User ID: {reader["UserID"]} \n Name: {reader["Name"]}");
                    }
                }
            }
        }

        // 6. ISSUE BOOK
        public void IssueBook(int userId, int bookId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string insertTx = "INSERT INTO Transactions (UserID, BookID, IssueDate) VALUES (@uid, @bid, GETDATE())";
                string updateQty = "UPDATE Books SET Quantity = Quantity - 1 WHERE BookID = @bid AND Quantity > 0";

                using (SqlCommand cmdTx = new SqlCommand(insertTx, conn))
                using (SqlCommand cmdQty = new SqlCommand(updateQty, conn))
                {
                    cmdTx.Parameters.AddWithValue("@uid", userId);
                    cmdTx.Parameters.AddWithValue("@bid", bookId);
                    cmdQty.Parameters.AddWithValue("@bid", bookId);

                    try
                    {
                        int rows = cmdQty.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            cmdTx.ExecuteNonQuery();
                            Console.WriteLine("\n Book issued successfully!");
                        }
                        else
                        {
                            Console.WriteLine("\n Book is out of stock or does not exist.");
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"\n Database Error: {ex.Message}");
                    }
                }
            }
        }

        // 7. RETURN BOOK
        public void ReturnBook(int userId, int bookId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string updateTx = "UPDATE Transactions SET ReturnDate = GETDATE() WHERE UserID = @uid AND BookID = @bid AND ReturnDate IS NULL";
                string updateQty = "UPDATE Books SET Quantity = Quantity + 1 WHERE BookID = @bid";

                using (SqlCommand cmdTx = new SqlCommand(updateTx, conn))
                using (SqlCommand cmdQty = new SqlCommand(updateQty, conn))
                {
                    cmdTx.Parameters.AddWithValue("@uid", userId);
                    cmdTx.Parameters.AddWithValue("@bid", bookId);
                    cmdQty.Parameters.AddWithValue("@bid", bookId);

                    try
                    {
                        int rows = cmdTx.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            cmdQty.ExecuteNonQuery();
                            Console.WriteLine("\n Book returned successfully!");
                        }
                        else
                        {
                            Console.WriteLine("\n No active issue record found for this user/book.");
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"\n Database Error: {ex.Message}");
                    }
                }
            }
        }

        // 8. VIEW HISTORY
        public void ViewHistory()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT t.TransactionID, u.Name, b.Title, t.IssueDate, t.ReturnDate 
                                 FROM Transactions t 
                                 JOIN Users u ON t.UserID = u.UserID 
                                 JOIN Books b ON t.BookID = b.BookID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("\n TRANSACTION HISTORY ");
                    if (!reader.HasRows)
                    {
                        Console.WriteLine("No history found.");
                        return;
                    }

                    while (reader.Read())
                    {
                        string returnStr = reader["ReturnDate"] == DBNull.Value ? "Not Returned" : Convert.ToDateTime(reader["ReturnDate"]).ToString("yyyy-MM-dd");
                        Console.WriteLine($"Tx #{reader["TransactionID"]} \n User: {reader["Name"]} \n Book: {reader["Title"]} \n Issued: {Convert.ToDateTime(reader["IssueDate"]):yyyy-MM-dd} | Returned: {returnStr}");
                    }
                }
            }
        }
    }
}
