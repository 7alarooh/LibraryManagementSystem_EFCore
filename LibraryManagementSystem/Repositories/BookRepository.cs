using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Repositories
{
    public class BookRepository : Repository<Book>
    {
        public BookRepository(ApplicationDbContext context) : base(context) { }

        public decimal GetTotalPrice()
        {
            return _context.Books.Sum(book => book.CopyPrice * book.TotalCopies);
        }

        public decimal GetMaxPrice()
        {
            return _context.Books.Max(book => book.CopyPrice);
        }

        public int GetTotalBorrowedBooks()
        {
            return _context.Books.Sum(book => book.Borrowed);
        }

        public int GetTotalBooksPerCategoryName(string categoryName)
        {
            return _context.Books.Where(book => book.Category.CName == categoryName)
                                 .Sum(book => book.TotalCopies);
        }
    }
}
