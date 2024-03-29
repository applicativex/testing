﻿using MediatR;
using System;

namespace Accounting.Domain.Queries
{
    public class GetAccountQuery : IRequest<GetAccountQueryResult>
    {
        public Guid AccountId { get; set; }
    }

    public class GetAccountQueryResult
    {
        public Guid Id { get; set; }

        public string Currency { get; set; }

        public string Description { get; set; }

        public decimal Balance { get; set; }
    }
}
