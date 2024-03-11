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
        private readonly FakeAuthorRepository _authorRepository;
        private readonly FakeBookRepository _bookRepository;

        public FakeUnitOfWork()
        {
            _authorRepository = new();
            _bookRepository = new();
        }

        public IRepository<Author> AuthorRepository => _authorRepository;

        public IRepository<Book> BookRepository => _bookRepository;

        public Task CreateDataBaseAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteDataBaseAsync()
        {
            throw new NotImplementedException();
        }

        public Task SaveAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}


