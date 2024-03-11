using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _253502.Application.Commands
{
    public sealed record AddAuthorCommand(string name, DateTime DateOfBirth, int FavoriteNumber) : IRequest<Author>
    { }
    public sealed record UpdateAuthorCommand(Author author) : IRequest<Author>
    { }
    internal class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, Author>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddAuthorCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Author> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            Author newAuthor = new Author(request.name, request.DateOfBirth, request.FavoriteNumber);
            await _unitOfWork.AuthorRepository.AddAsync(newAuthor, cancellationToken);
            await _unitOfWork.SaveAllAsync();
            return newAuthor;
        }
    }
    internal class UpdateAuthorCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateAuthorCommand, Author>
    {
        //private readonly IUnitOfWork _unitOfWork;
        //public UpdateAuthorCommandHandler(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //}
        public async Task<Author> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            //Author newAuthor = new Author(request.name, request.DateOfBirth, request.FavoriteNumber);
            await unitOfWork.AuthorRepository.UpdateAsync(request.author, cancellationToken);
            await unitOfWork.SaveAllAsync();
            return request.author;
        }
    }
}
