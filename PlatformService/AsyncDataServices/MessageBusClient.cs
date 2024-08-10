using System.Text;
using System.Text.Json;
using PlatformService.Dtos;
using RabbitMQ.Client;

namespace PlatformService.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageBusClient(IConfiguration configuration)
        {
            _configuration = configuration;
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQHost"],
                Port = int.Parse(_configuration["RabbitMQPort"])
            };
            try
            {

                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutDown;

                Console.WriteLine("Connected to Message Bus");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not connect to RabbitMq Bus:{ex.Message}");
            }
        }

        public void RabbitMQ_ConnectionShutDown(object sender, ShutdownEventArgs shutdownEventArgs)
        {
            Console.WriteLine("RabbitMQ connection Shut down");
        }

        public void PublisheNewPlatform(PlatformPublishedDto platformPublishedDto)
        {
            Console.WriteLine("Publish new Platform");
            var Message = JsonSerializer.Serialize(platformPublishedDto);
            if (_connection.IsOpen)
            {
                Console.WriteLine("Rabbit MQ connection Open. Sending message..");
                this.SendMessage(Message);
            }
            else
                Console.WriteLine("Rabbit MQ connection Closed. Not sending.");

        }

        private void SendMessage(string Message)
        {
            var body = Encoding.UTF8.GetBytes(Message);
            _channel.BasicPublish(exchange: "trigger",
            routingKey: "",
            basicProperties: null,
            body: body);

            Console.WriteLine($"We have sent {Message}");
        }

        public void Dispose()
        {
            Console.WriteLine($"Message bus disposed");
            if(_connection.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }

        }
    }
}