namespace CoingeckoLogic.Configs;

public class RedisConfig : BaseConfig
{
    public required string REDIS_PORT { get; set; }

    public required string REDIS_HOST { get; set; }

    public string Redis => $"{REDIS_HOST}:{REDIS_PORT},abortConnect=false";

    public RedisConfig() 
    {
        BaseConfig.Bind(this);
    }
}