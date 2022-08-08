using App.Worker;
using App.Worker.Repositories;
using App.Worker.Services;
using MediatR;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMediatR(typeof(Worker));
        services.AddTransient<IServiceBus, ServiceBus>();
        services.AddTransient<IPersonRepository, PersonRepository>();        
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
