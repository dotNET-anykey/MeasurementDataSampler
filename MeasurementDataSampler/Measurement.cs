namespace MeasurementDataSampler
{
    public class Measurement(DateTime measurementTime, MeasurementType type, double measurementValue)
    {
        public DateTime MeasurementTime { get; } = measurementTime;
        public double MeasurementValue { get; } = measurementValue;
        public MeasurementType Type { get; } = type;

        public override string ToString()
        {
            return $"{{{MeasurementTime:yyyy-MM-ddTHH:mm:ss}, {Type.ToString().ToUpper()}, {MeasurementValue}}}";
        }
    }
}
