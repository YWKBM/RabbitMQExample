using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace CoingeckoLogic.RabbitMQ;

public class RabbitMqService : IRabbitMqService
{
    private readonly Uri rabbimqUri = Configs.Config.RabbitMQ.RABBITMQ;

    public void SendMessage(object obj)
    {
        var message = JsonSerializer.Serialize(obj);
        SendMessage(message);
    }

    public void SendMessage(string message) 
    {
        var factory = new ConnectionFactory()
        {
            HostName = Configs.Config.RabbitMQ.RABBITMQ_HOST,
            UserName = Configs.Config.RabbitMQ.RABBITMQ_USER,
            Password = Configs.Config.RabbitMQ.RABBITMQ_PASS,
            Port = AmqpTcpEndpoint.UseDefaultPort
        };

        using (var connection = factory.CreateConnection())
        using (var chanel = connection.CreateModel())
        {
            chanel.QueueDeclare(queue: "MyQueue",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            chanel.BasicPublish(exchange: "",
                routingKey: "MyQueue",
                basicProperties: null,
                body: body);
        }
    }
}
