using MediatR;
using System;

namespace Accounting.Domain.Queries
{
    public class GetUserQuery : IRequest<GetUserQueryResult>
    {
        public Guid UserId { get; set; }
    }

    public class GetUserQueryResult
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }
}
