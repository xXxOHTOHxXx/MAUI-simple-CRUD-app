using _253502.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _253502.Persistence.Repository
{
    public class FakeUnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IRepository<Author> _authorRepository;
        private IRepository<Book> _bookRepository;

        public FakeUnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IRepository<Author> AuthorRepository
        {
            get
            {
                return _authorRepository ??= new FakeAuthorRepository();
            }
        }

        public IRepository<Book> BookRepository
        {
            get
            {
                return _bookRepository ??= new FakeBookRepository();
            }
        }

        public async Task SaveAllAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDataBaseAsync()
        {
            await _context.Database.EnsureDeletedAsync();
        }

        public async Task CreateDataBaseAsync()
        {
            await _context.Database.EnsureCreatedAsync();
        }
    }

}
