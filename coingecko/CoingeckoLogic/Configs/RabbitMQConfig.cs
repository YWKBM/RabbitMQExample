namespace CoingeckoLogic.Configs;

public class RabbitMQConfig : BaseConfig
{
    public string RABBITMQ_HOST { get; set; } = "rabbitmq";

    public string RABBITMQ_USER { get; set; } = "guest";

    public string RABBITMQ_PASS { get; set; } = "guest";

    public string RABBITMQ_VIRTUALHOST { get; set; } = string.Empty;

    public Uri RABBITMQ => new Uri($"amqp://{RABBITMQ_USER}:{RABBITMQ_PASS}@{RABBITMQ_HOST}/{RABBITMQ_VIRTUALHOST}");
}