using Shouldly;
using Xunit;

namespace MeasurementDataSampler.Tests
{
    public class MeasurementSamplerTests
    {
        [Fact]
        public void Sample_EmptyMeasurementsList_ReturnsEmptyDictionary()
        {
            var input = new List<Measurement>();
            var startOfSampling = new DateTime();

            var result = MeasurementSampler.Sample(startOfSampling, input);

            result.ShouldNotBeNull();
            result.ShouldBeEmpty();
        }

        [Fact]
        public void Sample_NoMeasurementsFromStartOfSampling_ReturnsEmptyDictionary()
        {
            var input = new List<Measurement>
            {
                new(new DateTime(2017, 01, 03, 10, 04, 45), MeasurementType.Temp, 35.79),
                new(new DateTime(2017, 01, 03, 10, 01, 18), MeasurementType.SpO2, 98.78),
                new(new DateTime(2017, 01, 03, 10, 09, 07), MeasurementType.Temp, 35.01),
                new(new DateTime(2017, 01, 03, 10, 03, 34), MeasurementType.SpO2, 96.49),
                new(new DateTime(2017, 01, 03, 10, 02, 01), MeasurementType.Temp, 35.82),
                new(new DateTime(2017, 01, 03, 10, 05, 00), MeasurementType.SpO2, 97.17),
                new(new DateTime(2017, 01, 03, 10, 05, 01), MeasurementType.SpO2, 95.08)
            };
            var startOfSampling = new DateTime(2018, 01, 01, 01, 01, 01);

            var result = MeasurementSampler.Sample(startOfSampling, input);

            result.ShouldNotBeNull();
            result.ShouldBeEmpty();
        }

        [Fact]
        public void Sample_ValidInput_SamplesMeasurements()
        {
            var input = new List<Measurement>
            {
                new(new DateTime(2017, 01, 03, 10, 04, 45), MeasurementType.Temp, 35.79),
                new(new DateTime(2017, 01, 03, 10, 01, 18), MeasurementType.SpO2, 98.78),
                new(new DateTime(2017, 01, 03, 10, 09, 07), MeasurementType.Temp, 35.01),
                new(new DateTime(2017, 01, 03, 10, 03, 34), MeasurementType.SpO2, 96.49),
                new(new DateTime(2017, 01, 03, 10, 02, 01), MeasurementType.Temp, 35.82),
                new(new DateTime(2017, 01, 03, 10, 05, 00), MeasurementType.SpO2, 97.17),
                new(new DateTime(2017, 01, 03, 10, 05, 01), MeasurementType.SpO2, 95.08)
            };
            var startOfSampling = new DateTime(2017, 01, 03, 10, 00, 01);

            var result = MeasurementSampler.Sample(startOfSampling, input);

            result.ShouldNotBeNull();
            result.ShouldNotBeEmpty();
            result.Count.ShouldBe(2);

            var tempMeasurements = result[MeasurementType.Temp];
            var sp02Measurements = result[MeasurementType.SpO2];

            tempMeasurements.Count.ShouldBe(2);
            sp02Measurements.Count.ShouldBe(2);
            tempMeasurements[0].MeasurementTime.ShouldBeLessThan(tempMeasurements[1].MeasurementTime);
            sp02Measurements[0].MeasurementTime.ShouldBeLessThan(sp02Measurements[1].MeasurementTime);
        }
    }
}