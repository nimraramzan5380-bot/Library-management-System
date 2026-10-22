using Library;
using System.Collections.Generic;
public class User
{
    private int userId;
    private string name;
    private List<string> issuedBooks;

    public User(int userId, string name)
    {
        this.userId = userId;
        this.name = name;
        this.issuedBooks = new List<string>();
    }
    public int GetId()
    {
        return userId;
    }
    public string Getname()
    {
        return name;
    }
    public void IssueBook(string bookTitle)
    {
        issuedBooks.Add(bookTitle);
    }

    public bool ReturnBook(string title)
    {
        if (issuedBooks.Contains(title))
        {
            issuedBooks.Remove(title);
            return true;
        }

        Console.WriteLine("\n Error: This book was not issued to the user.");
        return false;
    }
    public void DisplayInfo()
    {
        string books;

        if (issuedBooks.Count > 0)
        {
            books = string.Join(", ", issuedBooks);
        }
        else
        {
            books = "None";
        }

        Console.WriteLine($"User ID: {userId} \n Name: {name} \n Issued: [{books}]");
    }
    public override string ToString()
    {
        string books = "None";

        if (issuedBooks.Count > 0)
        {
            books = string.Join(", ", issuedBooks);
        }

        return $"ID: ({userId}) \n Name: {name} \n Issued Books: {books}";
    }
}