using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace DavChatBot.Services.RabbitMqServices
{
    public class RabbitMqService : IRabbitMqService
    {
        private const string RabbitMqConnectionStringName = "RabbitMq";
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly ILogger<RabbitMqService> _logger;
        public RabbitMqService(IConfiguration configuration, ILogger<RabbitMqService> logger)
        {
            _logger = logger;
            var connectionString = configuration.GetConnectionString(RabbitMqConnectionStringName);

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("RabbitMq connection string is empty");

            IConnectionFactory connectionFactory = new ConnectionFactory { Uri = new Uri(connectionString) };

            Console.WriteLine($"Connecting in {connectionFactory.Uri}");
            _connection = connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public async Task Consume<T>(string queue, Func<T, CancellationToken, Task> execute, CancellationToken ct = default)
        {
            await Task.CompletedTask;
            _logger.LogInformation("Consuming message from queue : {queue}", queue);
            _channel.QueueDeclare(queue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (sender, e) =>
            {
                var body = e.Body.ToArray();
                var queueObject = JsonSerializer.Deserialize<T>(body);
                if (queueObject is not null)
                    await execute(queueObject, ct);
            };

            _channel.BasicConsume(queue, true, consumer);
        }

        public async Task Produce<T>(string queue, T message, CancellationToken ct = default)
        {
            await Task.CompletedTask;
            _logger.LogInformation("Sending Message to Queue: {queue}", queue);
            _channel.QueueDeclare(queue,
               durable: true,
               exclusive: false,
               autoDelete: false,
               arguments: null
           );
            ReadOnlyMemory<byte> body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
            _channel.BasicPublish("", queue, null, body);
        }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}