using SimpleWix.WorkerTcpClient;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<EchoWorker>();
    })
    .Build();

await host.RunAsync();
