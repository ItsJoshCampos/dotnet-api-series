using Microsoft.Extensions.DependencyInjection;

namespace article_2_environment_variables;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.Configure<SampleClass>(builder.Configuration.GetSection(nameof(SampleClass)));

        var app = builder.Build();

        var vs_env = Environment.GetEnvironmentVariable("VS_ENV");
        var macos_env = Environment.GetEnvironmentVariable("MACOS_ENV");

        var dict = Environment.GetEnvironmentVariables();
        //Add Breakpoint on next line
        foreach (var d in dict)
        {
            Console.WriteLine(d);
        }

        var appsettings_env_sq_brackets = builder.Configuration["MyENV"];
        var appsettings_env_get_value = builder.Configuration.GetValue<string>("MyENV");

        

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}

