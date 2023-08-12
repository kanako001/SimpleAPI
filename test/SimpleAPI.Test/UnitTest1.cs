using SmipleAPI.Controllers;
using System;
using System.Linq;
using Moq;
using Xunit;
using Microsoft.Extensions.Logging;
using SmipleAPI;

namespace SimpleAPI.Test;

public class WeatherForecastControllerTests
{
    [Fact]
    public void Get_ReturnsWeatherForecasts()
    {
        // Arrange
        var mockSummaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
        var mockLogger = new Mock<ILogger<WeatherForecastController>>();
        var mockRandom = new Mock<Random>();
        mockRandom.Setup(r => r.Next(It.IsAny<int>(), It.IsAny<int>()))
                  .Returns((int min, int max) => new Random().Next(min, max));

        var controller = new WeatherForecastController(mockLogger.Object);
        var expectedCount = 5;

        // Act
        var result = controller.Get();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedCount, result.Count());

        foreach (var forecast in result)
        {
            Assert.IsType<WeatherForecast>(forecast);
            Assert.InRange(forecast.TemperatureC, -20, 55);
            Assert.Contains(forecast.Summary, mockSummaries);
            // Assert.InRange(forecast.Date, DateTime.Today, DateTime.Today.AddDays(expectedCount));
        }
    }
}