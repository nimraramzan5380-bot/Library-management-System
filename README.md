                                         Library Management System

"A database-driven Library Management System built with C# and .NET, featuring persistent storage using SQL Server LocalDB."



 Features

1- Admin Module
                   Add New Books: Insert books with unique IDs, titles, authors, and initial quantities
                   Display all stored books directly from LibraryDB.
                   Search catalog entries by Title or Author using SQL wildcard searches.
                   User Managememt: Register new library users and view registered user profiles.
                   Issue & Return Books: Issue copies to registered users and process returns with automatic inventory updates.
                   Transaction History: Display audit trails of book issuances and returns.

2- User Module
                    Browse Catalog: View available books in real time.
                    Search Library: Find specific titles or authors before visiting.

3- Tech Stack & Prerequisites

  Language: C#
  Framework:.NET Console Application
  Database: Microsoft SQL Server LocalDB (`(localdb)\MSSQLLocalDB`)
  Data Access: ADO.NET (`Microsoft.Data.SqlClient`)
  IDE: Visual Studio 2026 / Visual Studio 2022
  Version Control: Git & GitHub

4- Database Architecture

The application persists data across runs using three core tables in LibraryDB:

Books: BookID (PK), Title, Author, Quantity
Users: UserID (PK), Name
Transactions: TransactionID (PK, Identity), UserID (FK), BookID (FK), IssueDate, ReturnDate

                                                                Setup & Installation

1. Clone the Repository:
   
   git clone [https://github.com/nimraramzan5380-bot/Library-management-System.git](https://github.com/nimraramzan5380-bot/Library-management-System.git)
   cd Library-management-System
