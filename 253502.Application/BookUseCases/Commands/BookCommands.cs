using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _253502.Application.Commands
{
    public sealed record AddBookCommand(string Name, DateTime DateOfPublishment, int? GroupId, double rating=0) : IRequest<Book>
    { }
    public sealed record UpdateBookCommand(Book book, int? GroupId) : IRequest<Book>
    { }
    internal class AddBookCommandHandler : IRequestHandler<AddBookCommand, Book>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddBookCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Book> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            Book newBook = new Book(new BookInfo(request.Name, request.DateOfPublishment), request.rating);
            if (request.GroupId.HasValue)
            {
                newBook.AddToAuthor(request.GroupId.Value);
            }
            await _unitOfWork.BookRepository.AddAsync(newBook, cancellationToken);
            await _unitOfWork.SaveAllAsync();
            return newBook;
        }
    }
    internal class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Book>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateBookCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Book> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            //if (request.GroupId.HasValue)
            //{
            //    request.book.AddToAuthor(request.GroupId.Value);
            //}
            await _unitOfWork.BookRepository.UpdateAsync(request.book, cancellationToken);
            await _unitOfWork.SaveAllAsync();
            return request.book;
        }
    }
}
