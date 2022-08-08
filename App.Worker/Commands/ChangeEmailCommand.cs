#nullable disable
using FluentResults;
using MediatR;

namespace App.Worker.Commands
{
    public class ChangeEmailCommand : IRequest<Result>
    {
        public Guid UserId { get; set; }
        public string newEmail { get; set; }
    }
}