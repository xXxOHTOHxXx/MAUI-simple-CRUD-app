using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _253502.Application.Queries
{
    public class GetAuthorsRequest : IRequest<List<Author>> { }

    public class GetAuthorsRequestHandler : IRequestHandler<GetAuthorsRequest, List<Author>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAuthorsRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Author>> Handle(GetAuthorsRequest request, CancellationToken cancellationToken)
        {
            return (List<Author>)await _unitOfWork.AuthorRepository.ListAllAsync(cancellationToken);
        }
    }
}
