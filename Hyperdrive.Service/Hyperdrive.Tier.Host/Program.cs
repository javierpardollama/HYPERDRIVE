using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.Hyperdrive_Service>("api");

builder.Build().Run();
