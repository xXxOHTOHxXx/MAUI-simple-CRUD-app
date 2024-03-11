using _253502.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _253502.Application
{
    public static class DbInitializer
    {
        public static async Task Initialize(IServiceProvider services)
        {
            var unitOfWork = services.GetRequiredService<IUnitOfWork>();

            await unitOfWork.DeleteDataBaseAsync();
            await unitOfWork.CreateDataBaseAsync();

            IReadOnlyList<Author> authors = new List<Author>()//FIX LATER
            {
                new ("Абоб Бабоба", DateTime.Parse("12-01-2004"), 69),
                new("Бимбим Бамбам", DateTime.Parse("18-11-1984"), 420),
            };

            foreach (Author author in authors)
                await unitOfWork.AuthorRepository.AddAsync(author);

            await unitOfWork.SaveAllAsync();

            foreach (Author author in authors)
            {
                for (int j = 0; j < 6; j++)
                {
                    var book = new Book(new BookInfo($"Book {j++} of author {author.Name}", DateTime.Now.AddYears(-Random.Shared.Next(30))), Random.Shared.NextDouble() * 10);
                    book.AddToAuthor(author.Id);
                    await unitOfWork.BookRepository.AddAsync(book);
                }
            }

            await unitOfWork.SaveAllAsync();
        }

    }
}
