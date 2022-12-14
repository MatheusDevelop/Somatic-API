using Microsoft.EntityFrameworkCore;
using UI.Aws.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add AWS Lambda support. When application is run in Lambda Kestrel is swapped out as the web server with Amazon.Lambda.AspNetCoreServer. This
// package will act as the webserver translating request and responses between the Lambda event source and ASP.NET Core.
builder.Services.AddAWSLambdaHosting(LambdaEventSource.RestApi);
builder.Services.AddDbContext<SomaticContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("main")));
var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors(s => s.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
app.MapControllers();

app.MapGet("/", () => "v1.1");

app.Run();
