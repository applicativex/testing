using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Domain.Queries
{
    public class GetAccountEntriesQuery : IRequest<GetAccountEntriesQueryResult>
    {
        public Guid AccountId { get; set; }
    }

    public class GetAccountEntriesQueryResult
    {   
        public AccountEntryDto[] Entries { get; set; }
    }

    public class AccountEntryDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; }
        public DateTimeOffset EntryDate { get; }
    }
}
