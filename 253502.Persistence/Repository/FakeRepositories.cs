using _253502.Domain.Abstractions;
using _253502.Domain.Entities;
using System.Linq.Expressions;

namespace _253502.Persistence.Repository
{
    public class FakeAuthorRepository : IRepository<Author>
    {
        private readonly List<Author> _authors;

        public FakeAuthorRepository()
        {
            _authors = new List<Author>();

            var author = new Author("Абоб Бабоба", DateTime.Parse("12-01-2004"), 69);
            author.Id = 1;
            _authors.Add(author);
            author = new Author("Бимбим Бом",
            DateTime.Parse("18-11-1984"), 420);
            author.Id = 2;
            _authors.Add(author);
        }

        public Task AddAsync(Author entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Author entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Author> FirstOrDefaultAsync(Expression<Func<Author, bool>> filter, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Author> GetByIdAsync(int id, CancellationToken cancellationToken = default, params Expression<Func<Author, object>>[]? includesProperties)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Author>> ListAllAsync(CancellationToken cancellationToken = default)
        {
            return Task.Run(() => _authors as IReadOnlyList<Author>);
        }

        public Task<IReadOnlyList<Author>> ListAsync(Expression<Func<Author, bool>> filter, CancellationToken cancellationToken = default, params Expression<Func<Author, object>>[]? includesProperties)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Author entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }

    public class FakeBookRepository : IRepository<Book>
    {
        List<Book> _books = new List<Book>();

        public FakeBookRepository()//UNFIN
        {
            int k = 1;
            for (int i = 1; i <= 2; i++)
                for (int j = 0; j < 10; j++)
                {
                    var book = new Book(new BookInfo($"Book {k++}", DateTime.Now.AddYears(-Random.Shared.Next(30))), Random.Shared.NextDouble() * 10);
                    book.AddToAuthor(i);
                    _books.Add(book);
                }

        }

        public Task AddAsync(Book entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Book entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Book> FirstOrDefaultAsync(Expression<Func<Book, bool>> filter, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Book> GetByIdAsync(int id, CancellationToken cancellationToken = default, params Expression<Func<Book, object>>[]? includesProperties)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Book>> ListAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Book>> ListAsync(Expression<Func<Book, bool>> filter, CancellationToken cancellationToken = default, params Expression<Func<Book, object>>[]? includesProperties)
        {
            IQueryable<Book> data = _books.AsQueryable();
            return Task.Run(() => data.Where(filter).ToList() as IReadOnlyList<Book>);
        }

        public Task UpdateAsync(Book entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
