using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Repositories
{
    public class BorrowingRepository : Repository<Borrowing>
    {
        public BorrowingRepository(ApplicationDbContext context) : base(context) { }
    }
}
