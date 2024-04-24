using Marten;
using Oakton;
using Wolverine;
using Wolverine.Marten;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseWolverine();
builder.Services
    .AddMarten(options => options.Connection(builder.Configuration.GetConnectionString("Database")!))
    .IntegrateWithWolverine();

builder.Host.ApplyOaktonExtensions();

var app = builder.Build();

app.RunOaktonCommandsSynchronously(args);