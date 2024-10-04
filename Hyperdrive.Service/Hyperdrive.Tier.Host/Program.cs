using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var service = builder.AddProject<Projects.Hyperdrive_Service>("service");

var client = builder.AddNpmApp("client", "./../../Hyperdrive.Client");

builder.Build().Run();
