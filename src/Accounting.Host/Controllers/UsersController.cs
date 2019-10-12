using System;
using System.Threading.Tasks;
using Accounting.Domain.Commands;
using Accounting.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.Host.Controllers
{
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserAccount(Guid userId)
        {
            var result = await _mediator.Send(new GetUserQuery
            {
                UserId = userId
            });
            return Ok(result);
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateUser(CreateUserAccountRequest request)
        {
            var result = await _mediator.Send(new CreateUserCommand
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email
            });
            return Ok(result.UserId);
        }
    }
}