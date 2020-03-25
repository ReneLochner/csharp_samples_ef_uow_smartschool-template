using Microsoft.EntityFrameworkCore;
using SmartSchool.Core.Contracts;
using SmartSchool.Core.Entities;
using System.Linq;

namespace SmartSchool.Persistence
{
    public class MeasurementRepository : IMeasurementRepository
    {
        private ApplicationDbContext _dbContext;

        public MeasurementRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public  void AddRange(Measurement[] measurements)
        {
            _dbContext.Measurements.AddRange(measurements);
        }

        public Measurement[] GetMeasurementByLocationAndName(string location, string name)
        {
            return _dbContext
                    .Measurements
                    .Include(sensor => sensor.Sensor)
                    .Where(sensor => sensor.Sensor.Location.Equals(location) && sensor.Sensor.Name.Equals(name))
                    .OrderByDescending(measurement => measurement.Value)
                    .ThenByDescending(measurement => measurement.Time)
                    .Take(3)
                    .ToArray();
        }

        public int GetMeasurementCountByLocationAndName(string location, string name)
        {
            return _dbContext
                   .Measurements
                   .Include(measurement => measurement.Sensor)
                   .Where(measurement => measurement.Sensor.Location.Equals(location) && measurement.Sensor.Name.Equals(name))
                   .Count();

        }

        public double GetCo2MeasurementsAvgByLocationAndRange(string location, int min, int max)
        {
            return _dbContext
                    .Measurements
                    .Include(sensor => sensor.Sensor)
                    .Where(sensor => sensor.Sensor.Location.Equals(location) && sensor.Sensor.Name.Equals("co2"))
                    .Where(sensor => sensor.Value > min && sensor.Value < max)
                    .Average(sensor => sensor.Value);
        }
    }
}