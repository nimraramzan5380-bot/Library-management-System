using Library;
using System;
using System.Xml.Linq;
namespace Library
{

    public class Book
    {
        private int bookId, copies;
        private string title, author;

        public Book(int bookId, string title, string author, int copies)
        {
            this.bookId = bookId;
            this.title = title;
            this.author = author;
            this.copies = copies;
        }

        public int GetId() => bookId;
        public string GetTitle() => title;
        public string GetAuthor() => author;
        public int GetCopies() => copies;

        public bool UpdateCopies(int amount)
        {
            if (copies + amount >= 0)
            {
                copies = copies + amount;
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"[ID: {bookId}] Title: '{title}' | Author: {author} | Available Copies: {copies}";
        }
    }

}