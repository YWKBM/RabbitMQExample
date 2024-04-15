using CoingeckoDb;
using CoingeckoDb.Entities;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using Microsoft.EntityFrameworkCore;

namespace CoingeckoLogic.Consumer;

public class QueueListener : BackgroundService
{
    private readonly IConnection connection;

    private readonly IModel chanel;

    private readonly IServiceProvider serviceProvider;

    public QueueListener(IServiceProvider serviceProvider) 
    {
        this.serviceProvider = serviceProvider;

        var factory = new ConnectionFactory() { HostName = "localhost" };

        connection = factory.CreateConnection();
        chanel = connection.CreateModel();

        chanel.QueueDeclare(queue: "MyQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(chanel);
        consumer.Received += async (ch, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            var coin = JsonConvert.DeserializeObject<Coin>(message)
                ?? throw new Exception("Ошибка сериализации");
            
            Console.WriteLine($"from queue got: {coin}");

            using (var scope = serviceProvider.CreateScope())
            {
                var db = scope.ServiceProvider.GetService<AppDbContext>()
                    ?? throw new Exception($"Can't find {nameof(AppDbContext)}");

                await db.Set<Coin>().AddAsync(coin);

                await db.SaveChangesAsync();
            }

            chanel.BasicAck(ea.DeliveryTag, false);
        };
        

        chanel.BasicConsume("MyQueue", false, consumer);

        await Task.CompletedTask;
    }

    public override void Dispose()
    {
        chanel.Close();
        connection.Close();
        base.Dispose();
    }

}
