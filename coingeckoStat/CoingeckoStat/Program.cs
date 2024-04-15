using CoingeckoLogic;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogic();

var app = builder.Build();

app.Services.ConfigureLogic();


app.Run();
