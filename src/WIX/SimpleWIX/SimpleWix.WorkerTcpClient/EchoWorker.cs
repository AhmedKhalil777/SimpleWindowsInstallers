using System.IO;
using System.Net.Sockets;
using System.Text;

namespace SimpleWix.WorkerTcpClient
{
    public class EchoWorker : BackgroundService
    {
        private readonly ILogger<EchoWorker> _logger;
        byte[] _bytes = new byte[1024];

        public EchoWorker(ILogger<EchoWorker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                using var client = new TcpClient();
                client.Connect("127.0.0.1", 20908);
                using NetworkStream networkStream = client.GetStream();
                networkStream.ReadTimeout = 100000000;

                var writer = new StreamWriter(networkStream);
                var x = 0;

                while (!stoppingToken.IsCancellationRequested)
                {
                var message = "Ahmed Khalil";
                byte[] bytes = Encoding.UTF8.GetBytes((++x)+" " +message);

 
                    networkStream.Write(bytes, 0, bytes.Length);
                    _logger.LogInformation(">> Send: " + message);


                    // To run outside the Synchrounous process
                    _ = Task.Run(() =>
                    {
                        var i = networkStream.Read(_bytes, 0, _bytes.Length);
                        var data = System.Text.Encoding.UTF8.GetString(_bytes, 0, i);
                        _logger.LogInformation(">> Recieved: " + data);
                    }, stoppingToken);

                    // run before previous code
                    await Task.Delay(1000, stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

        }
    }
}