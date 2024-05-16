using JasperFx.CodeGeneration;
using JasperFx.CodeGeneration.Commands;
using Marten;
using Marten.Events;
using Oakton;
using Oakton.Resources;
using Playground.Wolverine.Models;
using Wolverine;
using Wolverine.Marten;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine(builder.Environment.EnvironmentName);

builder.Host.UseWolverine(options =>
{
    options.Policies.AutoApplyTransactions();
    options.OptimizeArtifactWorkflow(TypeLoadMode.Static);
});
builder.Services.AddMarten(options =>
    {
        options.Connection(builder.Configuration.GetConnectionString("Database")!);
        options.Events.StreamIdentity = StreamIdentity.AsString;
        options.Projections.LiveStreamAggregation<ShoppingList>();
    })
    .UseLightweightSessions()
    .OptimizeArtifactWorkflow(TypeLoadMode.Static)
    .IntegrateWithWolverine()
    .EventForwardingToWolverine();

builder.Host.UseResourceSetupOnStartup();
builder.Services.AssertAllExpectedPreBuiltTypesExistOnStartUp();

builder.Host.ApplyOaktonExtensions();

var app = builder.Build();

app.RunOaktonCommandsSynchronously(args);

public partial class Program;