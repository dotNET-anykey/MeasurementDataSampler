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
                .GroupBy(static m => m.Type);

            foreach (IGrouping<MeasurementType, Measurement> group in groupedMeasurements)
            {
                IEnumerable<IGrouping<DateTime, Measurement>> intervals = group.GroupBy(static m => new DateTime(
                    m.MeasurementTime.Year, m.MeasurementTime.Month, m.MeasurementTime.Day, m.MeasurementTime.Hour, 
                    m.MeasurementTime.Minute / MeasureInterval * MeasureInterval, 0));
                List<Measurement> measurements = intervals
                    .Select(static interval => interval
                        .OrderBy(static m => m.MeasurementTime)
                        .Last())
                    .OrderBy(static m => m.MeasurementTime)
                    .ToList();

                sampledMeasurements.Add(group.Key, measurements);
            }

            return sampledMeasurements;
        }
    }
}
