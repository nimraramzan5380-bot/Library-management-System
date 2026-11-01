using System;
using System.Collections.Generic;

namespace Library
{
    public class AdminMenuHandler
    {
       
        public void MenuSystem(Library lib)
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
                        {
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
                        }

                    case "2":
                        {
                            lib.ViewBooks();
                            break;
                        }

                    case "3":
                        {
                            Console.Write("Enter Title or Author to search: ");
                            string q = Console.ReadLine();
                            lib.SearchBook(q);
                            break;
                        }

                    case "4":
                        {
                            Console.Write("Enter User ID (number): ");
                            int.TryParse(Console.ReadLine(), out int uid);

                            Console.Write("Enter User Name: ");
                            string name = Console.ReadLine();

                            lib.AddUser(uid, name);
                            break;
                        }

                    case "5":
                        {
                            lib.ViewUsers();
                            break;
                        }

                    case "6":
                        {
                            Console.Write("Enter User ID (number): ");
                            int.TryParse(Console.ReadLine(), out int issueUid);

                            Console.Write("Enter Book ID (number): ");
                            int.TryParse(Console.ReadLine(), out int issueBid);

                            lib.IssueBook(issueUid, issueBid);
                            break;
                        }

                    case "7":
                        {
                            Console.Write("Enter User ID (number): ");
                            int.TryParse(Console.ReadLine(), out int returnUid);

                            Console.Write("Enter Book ID (number): ");
                            int.TryParse(Console.ReadLine(), out int returnBid);

                            lib.ReturnBook(returnUid, returnBid);
                            break;
                        }

                    case "8":
                        {
                            lib.ViewHistory();
                            break;
                        }

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Invalid Choice.");
                        break;
                }
            }
        }
    }
}
