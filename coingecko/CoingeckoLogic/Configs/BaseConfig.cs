using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace CoingeckoLogic.Configs;

public class BaseConfig
{
    public static void Bind<T>(T instance) where T : class 
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: false)
            .AddEnvironmentVariables()
            .Build();

        config.Bind(instance);

        var context = new ValidationContext(instance);
        Validator.ValidateObject(instance, context, true);
    }
}
