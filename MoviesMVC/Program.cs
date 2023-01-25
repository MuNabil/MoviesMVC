var builder = WebApplication.CreateBuilder(args);

builder.RegisterServices(typeof(Program));


var app = builder.Build();

app.RegisterPipelineComponents(typeof(Program));
await app.SeedAndMigrate();

await app.RunAsync();
