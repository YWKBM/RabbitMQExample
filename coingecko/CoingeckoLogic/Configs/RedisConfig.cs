namespace CoingeckoLogic.Configs;

public class RedisConfig : BaseConfig
{
    public string REDIS_PORT { get; set; } = "3479";

    public string REDIS_HOST { get; set; } = "localhost";

    public string Redis => $"{REDIS_HOST}:{REDIS_PORT},abortConnect=false";

    public RedisConfig() 
    {
        BaseConfig.Bind(this);
    }
}