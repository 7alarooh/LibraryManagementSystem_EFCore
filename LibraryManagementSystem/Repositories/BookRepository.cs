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
        public class BorrowCountByBook
        {
            public int BookId { get; set; }  // Book ID
            public int Count { get; set; }   // Number of times the book has been borrowed
        }
        public class BorrowCountByAuthor
        {
            public string AuthorName { get; set; }
            public int Count { get; set; }
        }
        public class BorrowCountByUser
        {
            public int UserId { get; set; }
            public int Count { get; set; }
        }
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
        // Add New Book
        public void AddNewBook(Book book)
        {
            Insert(book);
        }

        // Display All Books
        public IEnumerable<Book> DisplayAllBooks()
        {
            return GetAll();
        }

        // Search for Book by Name or char
        public IEnumerable<Book> SearchBookByNameOrChar(string query)
        {
            return _context.Books.Where(b => b.BName.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        // Edit a Book
        public void EditBookByName(string name, Action<Book> updateAction)
        {
            UpdateByName(name, updateAction);
        }

        // Remove a Book by ID
        public void RemoveBookById(int id)
        {
            DeleteById(id);
        }

        // Total number of books in the library
        public int GetTotalBooks()
        {
            return _context.Books.Count();
        }

        // Total number of categories and book count per category
        public Dictionary<string, int> GetBookCountPerCategory()
        {
            return _context.Books
                .GroupBy(b => b.Category.CName)
                .ToDictionary(g => g.Key, g => g.Sum(b => b.TotalCopies));
        }

        // Total number of copies of all books
        public int GetTotalCopies()
        {
            return _context.Books.Sum(b => b.TotalCopies);
        }

        // Total number of returned books
        public int GetTotalReturnedBooks()
        {
            return _context.Borrowings.Count(b => b.IsReturned);
        }

        // Track how many times each book has been borrowed
        public List<BorrowCountByBook> GetBorrowCountByBook()
        {
            return _context.Borrowings
                .GroupBy(b => b.BookID)
                .Select(g => new BorrowCountByBook { BookId = g.Key, Count = g.Count() })  // Use BorrowCountByBook class
                .ToList();
        }

        // Track how many times each author has been borrowed
        public List<BorrowCountByAuthor> GetBorrowCountByAuthor()
        {
            return _context.Borrowings
                .Join(_context.Books, br => br.BookID, b => b.BookID, (br, b) => b.Author)
                .GroupBy(author => author)
                .Select(g => new BorrowCountByAuthor { AuthorName = g.Key, Count = g.Count() })  // Use the custom class
                .ToList();
        }

        // Track how many times each user has borrowed books
        public List<BorrowCountByUser> GetBorrowCountByUser()
        {
            return _context.Borrowings
                .GroupBy(b => b.UserID)
                .Select(g => new BorrowCountByUser { UserId = g.Key, Count = g.Count() })  // Use custom class
                .ToList();
        }

        // Update book borrow count
        public void UpdateBookBorrowCount(int bookId, int borrowCount)
        {
            var book = _context.Books.Find(bookId);
            if (book != null)
            {
                book.Borrowed += borrowCount;
                _context.SaveChanges();
            }
        }

        // Find book to get the author
        public string GetAuthorByBookId(int bookId)
        {
            return _context.Books.Where(b => b.BookID == bookId).Select(b => b.Author).FirstOrDefault();
        }

        // Find the most borrowed book
        public Book GetMostBorrowedBook()
        {
            return _context.Books.OrderByDescending(b => b.Borrowed).FirstOrDefault();
        }

        // Find the most requested author
        public string GetMostRequestedAuthor()
        {
            return GetBorrowCountByAuthor()
                .OrderByDescending(a => a.Count)
                .FirstOrDefault().AuthorName;
        }

        // Find the top 3 users who borrowed the most books
        public List<(int UserId, int BorrowCount)> GetTop3BorrowingUsers()
        {
            return GetBorrowCountByUser()  // This returns a List<BorrowCountByUser>
                .OrderByDescending(u => u.Count)
                .Take(3)
                .Select(u => (UserId: u.UserId, BorrowCount: u.Count))  // Convert to tuple
                .ToList();
        }
        // Method to get books along with their authors' names, with optional filters
        public List<(string BookName, string AuthorName)> GetBooksWithAuthors(string bookName , string authorName)
        {
            var query = _context.Books.AsQueryable();

            // Apply filters if provided
            if (!string.IsNullOrEmpty(bookName))
            {
                query = query.Where(book => book.BName.Contains(bookName, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(authorName))
            {
                query = query.Where(book => book.Author.Contains(authorName, StringComparison.OrdinalIgnoreCase));
            }

            // Project to anonymous object, materialize, and map to tuple
            return query
                .Select(book => new { BookName = book.BName, AuthorName = book.Author }) // Use anonymous object
                .ToList() // Materialize the results
                .Select(book => (book.BookName, book.AuthorName)) // Map to tuple
                .ToList(); // Convert to a List of tuples
        }


        // Get books with high ratings (4-5)
        public IEnumerable<Book> GetHighRatedBooks()
        {
            return _context.Books.Where(b => b.Rating >= 4).ToList();
        }

        // Get books with low ratings (1-2)
        public IEnumerable<Book> GetLowRatedBooks()
        {
            return _context.Books.Where(b => b.Rating <= 2).ToList();
        }

        // Generate Sales Report
        public List<object> GetSalesReport()
        {
            return _context.PurchaseDetails
                .Join(_context.Books, pd => pd.BookID, b => b.BookID, (pd, b) => new { b.BName, pd.Quantity, b.CopyPrice })
                .GroupBy(data => data.BName)
                .Select(g => new
                {
                    BookName = g.Key,
                    Sales = g.Sum(x => x.Quantity * x.CopyPrice)
                })
                .ToList()
                .Cast<object>()
                .ToList();
        }

    }
}
