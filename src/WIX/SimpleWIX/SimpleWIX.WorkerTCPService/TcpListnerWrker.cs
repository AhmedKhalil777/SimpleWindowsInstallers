using System.Net.Sockets;

namespace SimpleWIX.WorkerTCPService
{
    public class TcpListnerWrker : BackgroundService
    {
        // Buffer for reading data
        byte[] _bytes = new byte[1024];
        private readonly ILogger<TcpListnerWrker> _logger;
        private readonly TcpListener _listener;
        public TcpListnerWrker(ILogger<TcpListnerWrker> logger, TcpListener tcpListener)
        {
            _logger = logger;
            _listener = tcpListener;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Woker of Tcp Started");
            // Perform a blocking call to accept requests.
            // You could also use server.AcceptSocket() here.
            TcpClient client = await _listener.AcceptTcpClientAsync();
            _logger.LogInformation("Connection from " + client?.Client?.RemoteEndPoint?.ToString() + " established @" + System.DateTime.UtcNow);

            // Get a stream object for reading and writing
            NetworkStream stream = client.GetStream();

            int i;
            // Loop to receive all the data sent by the client.
            i = stream.Read(_bytes, 0, _bytes.Length);
            while (!stoppingToken.IsCancellationRequested)
            { 

                if (i != 0)
                {
                    // Translate data bytes to a ASCII string.
                    var data = System.Text.Encoding.UTF8.GetString(_bytes, 0, i);
                    _logger.LogInformation(">> Recieved : " + data);


                    // Process the data sent by the client.
                    data = data.ToUpper();

                    byte[] msg = System.Text.Encoding.UTF8.GetBytes("Hello" + data);

                    // Send back a response.
                    stream.Write(msg, 0, msg.Length);
                    _logger.LogInformation(">> Send : " + data);

                    i = stream.Read(_bytes, 0, _bytes.Length);
                }

            }
        }
    }
}