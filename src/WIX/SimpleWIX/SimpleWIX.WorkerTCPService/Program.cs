using SimpleWIX.WorkerTCPService;

using System.Net;
using System.Net.Sockets;
using System.Reflection;

//Console Colors
var progInfo = ConsoleColor.Yellow;
var defaultColor = Console.ForegroundColor;
var connectionColor = ConsoleColor.Green;
var exceptionColor = ConsoleColor.Red;
var helpColor = ConsoleColor.Cyan;

Console.ForegroundColor = progInfo;
Console.WriteLine();
Console.WriteLine("---------------------------------------------------------");
Console.WriteLine(" Worker v{0} {1}", Assembly.GetExecutingAssembly().GetName().Version, " by Ahmed Khalil");
Console.WriteLine();
Console.WriteLine("---------------------------------------------------------");
Console.WriteLine();

// set the defaults
int port = 20908;
IPAddress addr = IPAddress.Any;


Console.ForegroundColor = helpColor;
Console.ForegroundColor = defaultColor;


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<TcpListener>(op => {
            var tcpListner = new TcpListener(addr, port);
            tcpListner.Start();
            return tcpListner;
        });
        services.AddHostedService<TcpListnerWrker>();
    })
    .Build();

await host.RunAsync();
