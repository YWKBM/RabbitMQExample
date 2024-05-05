namespace CoingeckoLogic.Configs;

public class PostgresConfig : BaseConfig
{
    public string DATABASE_HOST { get; set; } = "localhost";

    public string DATABASE_USER { get; set; } = string.Empty;

    public string DATABASE_PASS { get; set; } = string.Empty;

    public string DATABASE_NAME { get; set; } = string.Empty;

    public string DATABASE => $"Host={DATABASE_HOST};Database={DATABASE_NAME};Username={DATABASE_USER};Password={DATABASE_PASS};Timezone=0";


    public PostgresConfig() 
    {
        BaseConfig.Bind(this);
    }

}
