using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using TCPListener.Interfaces;

namespace TCPListener
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IEncryptor _encryptor;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public Worker(ILogger<Worker> logger, IEncryptor encryptor, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _encryptor = encryptor;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 8888);
            listener.Start();
            _logger.LogInformation("Server started. Waiting for connections...");

            while (!stoppingToken.IsCancellationRequested)
            {
                TcpClient client = await listener.AcceptTcpClientAsync(stoppingToken);
                ProcessClient(client);
            }
        }

        private async void ProcessClient(TcpClient client)
        {
            _logger.LogInformation("Client connected.");

            try
            {
                using (SslStream sslStream = new SslStream(client.GetStream(), false))
                {
                    X509Certificate2 certificate = new X509Certificate2("server.pfx", "password");
                    await sslStream.AuthenticateAsServerAsync(certificate, false, SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12, false);
                    byte[] buffer = new byte[1024];
                    int bytesRead = await sslStream.ReadAsync(buffer, 0, buffer.Length);
                    string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    _logger.LogInformation($"Received: {receivedData}");
                    var encryptedData = _encryptor.EncryptData(receivedData);
                    await SaveInDb(encryptedData);
                    byte[] responseBytes = Encoding.UTF8.GetBytes("Message received!");
                    await sslStream.WriteAsync(responseBytes, 0, responseBytes.Length);
                    _logger.LogInformation("Response sent.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred: {ex.Message}");
            }
            finally
            {
                client.Close();
                _logger.LogInformation("Client disconnected.");
            }
        }

        private async Task SaveInDb(string data)
        {
            using (var childScope = _serviceScopeFactory.CreateScope())
            {
                var dbManager = childScope.ServiceProvider.GetService<IDbManager>();
                await dbManager.AddEncryptedData(data);
            }
        }
    }
}
