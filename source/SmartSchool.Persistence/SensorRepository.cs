using Microsoft.EntityFrameworkCore;
using SmartSchool.Core.Contracts;
using System.Linq;

namespace SmartSchool.Persistence
{
    public class SensorRepository : ISensorRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SensorRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public (string name, string location, double avg)[] GetAllAvgSensors()
        {
            return _dbContext
                    .Sensors
                    .Include(sensor => sensor.Measurements)
                    .Select(sensor => new {
                        name = sensor.Name,
                        location = sensor.Location,
                        avg = sensor.Measurements.Average(measurement => measurement.Value)
                    })
                    .OrderBy(sensor => sensor.location)
                    .ThenBy(sensor => sensor.name)
                    .AsEnumerable()
                    .Select(sensor => (sensor.name, sensor.location, sensor.avg))
                    .ToArray();
        }
    }
}