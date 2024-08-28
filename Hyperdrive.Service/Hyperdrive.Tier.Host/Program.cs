using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var service = builder.AddProject<Projects.Hyperdrive_Service>("service");

builder.Build().Run();
