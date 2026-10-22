using System;
using System.Collections.Generic;

namespace Library
{
    internal class Library
    {
        private Dictionary<int, Book> books = new Dictionary<int, Book>();
        private Dictionary<int, User> users = new Dictionary<int, User>();         
        private List<string> history = new List<string>();

        public void AddBook(int bookId, string title, string author, int copies) 

        {
            if (books.ContainsKey(bookId))
            {
                Console.WriteLine($"\n Error: Book ID {bookId} already exists.");
            }
            else if (copies < 0)
            {
                Console.WriteLine("\n Error: Copies cannot be negative.");
            }
            else
            {
                books[bookId] = new Book(bookId, title, author, copies);
                Console.WriteLine($"\n Book '{title}' has been added successfully.");
            }
        }

        public void ViewBooks()
        {
            Console.WriteLine("\n Current Library Inventory ");

            if (books.Count == 0)
            {
                Console.WriteLine("The library is currently empty.");
                return;
            }

            foreach (Book book in books.Values)
            {
                Console.WriteLine(book);
            }
        }

        public void SearchBook(string query)
        {
            Console.WriteLine($"\n Search Results for '{query}' ");
            bool found = false;

            foreach (Book book in books.Values)
            {
                if (book.GetTitle().Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    book.GetAuthor().Contains(query, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine(book);
                    found = true;
                }
            }

            if (!found)
                Console.WriteLine("No matching books found.");
        }

        public void AddUser(int uId, string name)
        {
            if (users.ContainsKey(uId))
            {
                Console.WriteLine($"\n Error: User ID {uId} already registered.");
            }
            else
            {
                users[uId] = new User(uId, name);
                Console.WriteLine($"\n User '{name}' registered.");
            }
        }

        public void ViewUsers()
        {
            Console.WriteLine("\n Registered Users ");

            if (users.Count == 0)
            {
                Console.WriteLine("No registered users.");
                return;
            }

            foreach (User user in users.Values)
            {
                Console.WriteLine(user);
            }
        }

        public void IssueBook(int uId, int bookId)
        {
            if (!users.ContainsKey(uId))
            {
                Console.WriteLine("\n Error: User not found.");
                return;
            }

            if (!books.ContainsKey(bookId))
            {
                Console.WriteLine("\n Error: Book not found.");
                return;
            }

            Book book = books[bookId];
            User user = users[uId];

            if (book.UpdateCopies(-1))
            {
                user.IssueBook(book.GetTitle());
                history.Add($"User {uId} issued '{book.GetTitle()}'");

                Console.WriteLine(
                    $"\n[SUCCESS] '{book.GetTitle()}' issued to {user.Getname()}.");
            }
            else
            {
                Console.WriteLine("\n Error: No copies available.");
            }
        }

        public void ReturnBook(int uId, int bookId)
        {
            if (!users.ContainsKey(uId) || !books.ContainsKey(bookId))
            {
                Console.WriteLine("\n[!] Error: Invalid User or Book ID.");
                return;
            }

            Book book = books[bookId];
            User user = users[uId];

            if (user.ReturnBook(book.GetTitle()))
            {
                book.UpdateCopies(1);
                history.Add($"User {uId} returned '{book.GetTitle()}'");

                Console.WriteLine("\n[SUCCESS] Book returned successfully.");
            }
            else
            {
                Console.WriteLine("\n[!] Error: User does not have this book.");
            }
        }

        public void ViewHistory()
        {
            Console.WriteLine("\n Transaction History ");

            if (history.Count == 0)
            {
                Console.WriteLine("No transactions recorded.");
                return;
            }

            foreach (string log in history)
            {
                Console.WriteLine(log);
            }
        }
    }
}
