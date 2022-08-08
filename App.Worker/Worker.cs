using App.Worker.Commands;
using App.Worker.Entities;
using App.Worker.Messages;
using App.Worker.Services;
using FluentResults;
using MediatR;

namespace App.Worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceBus _bus;
    private readonly IMediator _mediator;

    public Worker(ILogger<Worker> logger, 
                  IServiceBus bus, 
                  IMediator mediator)
    {
        _bus = bus;
        _logger = logger;
        _mediator = mediator;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var message = await _bus.GetLastEmailChangedMessageAsync();

            var command = new ChangeEmailCommand
            {
                newEmail = message.newEmail,
                UserId = message.UserId
            };

            var result = await _mediator.Send(command);

            this.PrintResult(result);

            await Task.Delay(10000, stoppingToken);
        }
    }

    private void PrintResult(Result result)
    {
        _logger.LogInformation($"Is Success: {result.IsSuccess}");

        if(result.IsFailed)
            foreach(var error in result.Errors)
                _logger.LogInformation($"Error: {error.Message}");
        else    
            _logger.LogInformation($"There are no error messages");

    }
}
