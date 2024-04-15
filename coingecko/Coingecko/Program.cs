using CoingeckoLogic;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogic();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.Services.ConfigureLogic();

app.Run();


