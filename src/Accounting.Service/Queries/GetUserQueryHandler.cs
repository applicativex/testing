using Accounting.Domain;
using Accounting.Domain.Queries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Accounting.Service.Queries
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserQueryResult>
    {
        private readonly IUserRepository _userRepository;

        public GetUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUserQueryResult> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindAsync(request.UserId);
            return new GetUserQueryResult
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }
    }
}
