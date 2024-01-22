namespace MeasurementDataSampler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Measurement> input = new List<Measurement>
            {
                new(new DateTime(2017, 01, 03, 10, 04, 45), MeasurementType.Temp, 35.79),
                new(new DateTime(2017, 01, 03, 10, 01, 18), MeasurementType.SpO2, 98.78),
                new(new DateTime(2017, 01, 03, 10, 09, 07), MeasurementType.Temp, 35.01),
                new(new DateTime(2017, 01, 03, 10, 03, 34), MeasurementType.SpO2, 96.49),
                new(new DateTime(2017, 01, 03, 10, 02, 01), MeasurementType.Temp, 35.82),
                new(new DateTime(2017, 01, 03, 10, 05, 00), MeasurementType.SpO2, 97.17),
                new(new DateTime(2017, 01, 03, 10, 05, 01), MeasurementType.SpO2, 95.08)
            };

            Console.WriteLine("INPUT:");
            foreach (Measurement measurement in input)
            {
                Console.WriteLine(measurement.ToString());
            }
            
            Dictionary<MeasurementType, List<Measurement>> sample = MeasurementSampler.Sample(new DateTime(2017, 01, 03, 10, 00, 00), input);

            Console.WriteLine("OUTPUT:");
            foreach (Measurement measurement in sample.Values.SelectMany(measurements => measurements))
            {
                Console.WriteLine(measurement.ToString());
            }
        }
    }
}
