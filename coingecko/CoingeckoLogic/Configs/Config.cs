namespace CoingeckoLogic.Configs;

public static class Config
{
    public readonly static RedisConfig Redis = new();
    public readonly static RabbitMQConfig RabbitMQ = new();
}
