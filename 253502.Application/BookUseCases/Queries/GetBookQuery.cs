using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _253502.Application.Queries
{
    public sealed record GetBooksByGroupRequest(int Id) : IRequest<IEnumerable<Book>> { }

    internal class GetBooksByGroupHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetBooksByGroupRequest, IEnumerable<Book>>
    {
        private readonly IRepository<Book> _bookRepository;

        public async Task<IEnumerable<Book>> Handle(GetBooksByGroupRequest request, CancellationToken cancellationToken)
        {
            return await unitOfWork.BookRepository.ListAsync(t => t.AuthorID.Equals(request.Id), cancellationToken);
        }
    }
}
