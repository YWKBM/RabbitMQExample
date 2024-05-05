namespace CoingeckoLogic.Configs;

public static class Config
{
    public readonly static RabbitMQConfig RabbitMQ = new();

    public readonly static PostgresConfig Postgres = new();
}
