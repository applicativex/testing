using MediatR;
using System;

namespace Accounting.Domain.Queries
{
    public class GetAccountOperationsQuery : IRequest<GetAccountOperationsQueryResult>
    {
        public Guid AccountId { get; set; }
    }

    public class GetAccountOperationsQueryResult
    {   
        public AccountOperationDto[] Operations { get; set; }
    }

    public class AccountOperationDto
    {
        public Guid Id { get; set; }    
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string OperationType { get; set; }
        public string Description { get; set; } 
    }
}
