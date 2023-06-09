using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ZipStreaming.Providers;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.DescribeAllParametersInCamelCase();
});
services.AddRouting(options => options.LowercaseUrls = true);

services.AddSingleton<IDocumentsProvider, DocumentsProvider>();
services.AddHttpClient();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseRouting();
app.UseEndpoints(x => { x.MapControllers(); });

app.Run();