using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Domain.Commands
{
    public class CreateUserCommand : IRequest<CreateUserResult>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }

    public class CreateUserResult
    {
        public Guid UserId { get; set; }
    }
}
