using MediatR;
using System;

namespace Accounting.Domain.Queries
{
    public class GetUserAccountsQuery : IRequest<GetUserAccountsQueryResult>
    {
        public Guid UserId { get; set; }
    }

    public class GetUserAccountsQueryResult
    {
        public AccountDto[] Accounts { get; set; }
    }

    public class AccountDto
    {
        public Guid Id { get; set; }

        public string Currency { get; set; }

        public string Description { get; set; }

        public decimal Balance { get; set; }
    }
}
