using System;
using Library;

namespace LibrarySystem
{
    public class MenuSystem
    {
        private Library.Library lib;

        public MenuSystem()
        {
            lib = new Library.Library();

            // Seed data using numeric IDs (matching your existing Library.AddBook method)
            //lib.AddBook(101, "Python Mastery", "Jhon smith", 5);
            //lib.AddBook(102, "Data Science", "Nimra alvi", 2);
        }

        public void AdminMenu()
        {
            while (true)
            {
                Console.WriteLine("\n ADMIN MENU ");
                Console.WriteLine("1. Add Book      2. View Books");
                Console.WriteLine("3. Search Book   4. Add User");
                Console.WriteLine("5. View Users    6. Issue Book");
                Console.WriteLine("7. Return Book   8. View History");
                Console.WriteLine("0. Back to Main Menu");

                Console.Write("\nSelect Option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter Book ID (number): ");
                        int.TryParse(Console.ReadLine(), out int bid);

                        Console.Write("Enter Title: ");
                        string title = Console.ReadLine();

                        Console.Write("Enter Author: ");
                        string author = Console.ReadLine();

                        Console.Write("Enter Copies: ");
                        int.TryParse(Console.ReadLine(), out int qty);

                        lib.AddBook(bid, title, author, qty);
                        break;

                    case "2":
                        lib.ViewBooks();
                        break;

                    case "3":
                        Console.Write("Enter Title or Author to search: ");
                        string q = Console.ReadLine();
                        lib.SearchBook(q);
                        break;

                    case "4":
                        Console.Write("Enter User ID (number): ");
                        int.TryParse(Console.ReadLine(), out int uid);

                        Console.Write("Enter User Name: ");
                        string name = Console.ReadLine();

                        lib.AddUser(uid, name);
                        break;

                    case "5":
                        lib.ViewUsers();
                        break;

                    case "6":
                        Console.Write("Enter User ID (number): ");
                        int.TryParse(Console.ReadLine(), out int issueUid);

                        Console.Write("Enter Book ID (number): ");
                        int.TryParse(Console.ReadLine(), out int issueBid);

                        lib.IssueBook(issueUid, issueBid);
                        break;

                    case "7":
                        Console.Write("Enter User ID (number): ");
                        int.TryParse(Console.ReadLine(), out int returnUid);

                        Console.Write("Enter Book ID (number): ");
                        int.TryParse(Console.ReadLine(), out int returnBid);

                        lib.ReturnBook(returnUid, returnBid);
                        break;

                    case "8":
                        lib.ViewHistory();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Invalid Choice.");
                        break;
                }
            }
        }

        public void UserMenu()
        {
            while (true)
            {
                Console.WriteLine("\n USER MENU ");
                Console.WriteLine("1. View Available Books");
                Console.WriteLine("2. Search for a Book");
                Console.WriteLine("0. Back to Main Menu");

                Console.Write("\nSelect Option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        lib.ViewBooks();
                        break;

                    case "2":
                        Console.Write("Search Title/Author: ");
                        string q = Console.ReadLine();
                        lib.SearchBook(q);
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Invalid Choice.");
                        break;
                }
            }
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("\n LIBRARY MANAGEMENT SYSTEM ");
                Console.WriteLine("1. Admin Login");
                Console.WriteLine("2. User Access");
                Console.WriteLine("3. Exit");

                Console.Write("\nSelect System (number): ");
                string mainChoice = Console.ReadLine();

                if (mainChoice == "1")
                {
                    AdminMenu();
                }
                else if (mainChoice == "2")
                {
                    UserMenu();
                }
                else if (mainChoice == "3")
                {
                    Console.WriteLine("Exiting System... Goodbye!");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Choice.");
                }
            }
        }

    }
}
   



