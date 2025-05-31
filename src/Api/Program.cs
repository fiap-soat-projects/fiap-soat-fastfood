using Api.Middlewares;
using Application;
using Infrastructure;
using System.Text.Json.Serialization;

namespace Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var services = builder.Services;

        services
            .InjectInfrastructureDependencies()
            .InjectApplicationDependencies();

        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddControllers()
            .AddJsonOptions(options =>
            {
                var jsonStringEnumConverter = new JsonStringEnumConverter();

                options.JsonSerializerOptions.Converters.Add(jsonStringEnumConverter);
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

        var app = builder.Build();

        app.UseMiddleware<ErrorHandlingMiddleware>();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
