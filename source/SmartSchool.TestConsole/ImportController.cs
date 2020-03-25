using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SmartSchool.Core.Entities;
using Utils;

namespace SmartSchool.TestConsole
{
    public class ImportController
    {
        const string Filename = "measurements.csv";

        /// <summary>
        /// Liefert die Messwerte mit den dazugehörigen Sensoren
        /// </summary>
        public static IEnumerable<Measurement> ReadFromCsv()
        {
            bool isFirstRow = false;
            string filePath = MyFile.GetFullNameInApplicationTree(Filename);
            string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
            IList<Measurement> measurements = new List<Measurement>();
            IDictionary<string, Sensor> sensors = new Dictionary<string, Sensor>();

            foreach (var item in lines)
            {
                if(isFirstRow)
                {
                    string[] parts = item.Split(";");
                    string location = parts[2].Split("_")[0];
                    string name = parts[2].Split("_")[1];

                    DateTime dateTime = DateTime.Parse($"{parts[0]} {parts[1]}");
                    Measurement measurement = new Measurement() { Time = dateTime, Value = Convert.ToDouble(parts[3]) };
                    Sensor sensor;

                    if (!sensors.TryGetValue(parts[2], out sensor))
                    {
                        Sensor newSensor = new Sensor() { Name = name, Location = location };
                        measurement.Sensor = newSensor;
                        newSensor.Measurements.Add(measurement);
                        sensors.Add(parts[2], newSensor);
                    }
                    else
                    {
                        measurement.Sensor = sensor;
                        sensor.Measurements.Add(measurement);
                    }
                    measurements.Add(measurement);
                }
            }

            return measurements;
        }

    }
}
