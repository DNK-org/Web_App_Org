using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text.Json;

namespace Azure_API_Test.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpGet("getDataTable")]
        public IActionResult GetDataTable()
        {
            // Create DataTable
            DataTable table = new DataTable("SampleTable");

            // Add Columns
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Email", typeof(string));
            table.Columns.Add("City", typeof(string));

            // Add Rows
            table.Rows.Add(1, "John Doe", "john.doe@example.com", "New York");
            table.Rows.Add(2, "Jane Smith", "jane.smith@example.com", "Los Angeles");

            var dataList = new List<Dictionary<string, object>>();
            foreach (DataRow row in table.Rows)
            {
                var rowDict = new Dictionary<string, object>();
                foreach (DataColumn column in table.Columns)
                {
                    rowDict[column.ColumnName] = row[column];
                }
                dataList.Add(rowDict);
            }

            // Serialize List to JSON
            return Ok(JsonSerializer.Serialize(dataList));
        }
    }
}
