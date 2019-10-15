using Accounting.Domain;
using Accounting.Domain.Queries;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Accounting.Service.Queries
{
    public class GetAccountOperationsQueryHandler : IRequestHandler<GetAccountOperationsQuery, GetAccountOperationsQueryResult>
    {
        private readonly IAccountOperationQueryRepository _accountOperationQueryRepository;

        public GetAccountOperationsQueryHandler(IAccountOperationQueryRepository accountOperationQueryRepository)
        {
            _accountOperationQueryRepository = accountOperationQueryRepository;
        }

        public async Task<GetAccountOperationsQueryResult> Handle(GetAccountOperationsQuery request, CancellationToken cancellationToken)
        {
            var operations = await _accountOperationQueryRepository.GetAccountOperationsAsync(request.AccountId);
            return new GetAccountOperationsQueryResult
            {
                Operations = operations.Select(x => new AccountOperationDto
                {
                    Id = x.Id,
                    AccountId = x.AccountId,
                    Amount = x.Amount,
                    CreatedAt = x.CreatedAt,
                    Description = x.Description,
                    OperationType = x.OperationType.ToString()
                }).ToArray()
            };
        }
    }
}
