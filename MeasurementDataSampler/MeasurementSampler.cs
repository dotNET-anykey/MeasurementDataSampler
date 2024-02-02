namespace MeasurementDataSampler
{
    public static class MeasurementSampler
    {
        private const int MeasureInterval = 5;

        public static Dictionary<MeasurementType, List<Measurement>> Sample(DateTime startOfSampling, List<Measurement> unsampledMeasurements)
        {
            Dictionary<MeasurementType, List<Measurement>> sampledMeasurements = new Dictionary<MeasurementType, List<Measurement>>();
            IEnumerable<IGrouping<MeasurementType, Measurement>> groupedMeasurements = unsampledMeasurements
                .Where(x => x.MeasurementTime >= startOfSampling)
                .GroupBy(m => m.Type);

            foreach (IGrouping<MeasurementType, Measurement> group in groupedMeasurements)
            {
                IEnumerable<IGrouping<DateTime, Measurement>> intervals = group.GroupBy(m => {
                    var adjustedTime = m.MeasurementTime.Second == 0 && m.MeasurementTime.Minute % MeasureInterval == 0
                        ? m.MeasurementTime.AddSeconds(-1) : m.MeasurementTime;

                    return new DateTime(
                        adjustedTime.Year, adjustedTime.Month, adjustedTime.Day, adjustedTime.Hour,
                        adjustedTime.Minute / MeasureInterval * MeasureInterval, 0, DateTimeKind.Utc);
                });

                List<Measurement> measurements = intervals
                    .Select(interval => interval
                        .OrderBy(m => m.MeasurementTime)
                        .Last())
                    .OrderBy(m => m.MeasurementTime)
                    .ToList();

                sampledMeasurements.Add(group.Key, measurements);
            }

            return sampledMeasurements;
        }
    }
}
