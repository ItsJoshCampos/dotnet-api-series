using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace article_2_environment_variables.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly SampleClass _sampleClass;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IOptions<SampleClass> sampleClassOptions)
    {
        _logger = logger;
        _sampleClass = sampleClassOptions.Value;
    }

    [HttpGet]
    [Route("")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }


    [HttpGet]
    [Route("SampleClass")]
    public SampleClass GetSampleClass()
    {
        return _sampleClass;
    }
}

