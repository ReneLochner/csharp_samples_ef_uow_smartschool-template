using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace SmartSchool.Core.Entities
{
    public class Measurement : EntityObject
    {

        public int SensorId { get; set; }

        [ForeignKey(nameof(SensorId))]
        public Sensor Sensor { get; set; }

        public DateTime Time { get; set; }
        public double Value { get; set; }

        public override string ToString()
        {
            return $"{nameof(Sensor)}: {Sensor.Name}, {nameof(Time)}: {Time}, {nameof(Value)}: {Value}";
        }
    }
}
