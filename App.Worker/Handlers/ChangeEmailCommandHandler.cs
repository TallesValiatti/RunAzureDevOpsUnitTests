using App.Worker.Commands;
using App.Worker.Repositories;
using FluentResults;
using MediatR;

namespace App.Worker.Handlers
{
    public class ChangeEmailCommandHandler : IRequestHandler<ChangeEmailCommand, Result>
    {
        private readonly IPersonRepository _personRepository;
        public ChangeEmailCommandHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        public async Task<Result> Handle(ChangeEmailCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var person = await _personRepository.GetByIdAsync(command.UserId);
               
                if(person is null)
                    return Result.Fail("The person does not exist");

                var changeEmailResult = person.ChangeEmail(command.newEmail);
                
                if(changeEmailResult.IsFailed)
                    return changeEmailResult;

                await _personRepository.UpdateAsync(person);

                return Result.Ok();
            }
            catch (System.Exception ex)
            {
                return Result.Fail($"Unexpected error: {ex.Message}");
            }
        }
    }
}