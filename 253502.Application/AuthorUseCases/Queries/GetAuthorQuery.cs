using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _253502.Application.AuthorUseCases.Queries
{
    public class GetAuthorQuery : IRequest<List<Author>>
    {
    }

    public class GetGroupsHandler : IRequestHandler<GetAuthorGruops, List<Group>>
    {
        private readonly IGroupRepository _groupRepository;

        public GetGroupsHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<List<Group>> Handle(GetGroupsQuery request, CancellationToken cancellationToken)
        {
            return await _groupRepository.GetAllAsync();
        }
    }
}
