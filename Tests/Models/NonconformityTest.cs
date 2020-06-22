using NonconformityControl.Models;
using Xunit;

namespace NonconformityControl.Tests.Models
{
    public class NonconformityTest
    {
        [Fact]
        public void NewNonconformityShouldStartWithVersionOne()
        {
            Nonconformity nonconformity = new Nonconformity("Controlled materials stored without proper indication.");
            Assert.Equal(1, nonconformity.Version);
        }

        [Fact]
        public void ShouldUpdateCode()
        {
            Nonconformity nonconformity = new Nonconformity("Controlled materials stored without proper indication.");
            nonconformity.UpdateCode("2019:02:05");
            Assert.Equal("2019:02:05", nonconformity.Code);
        }

        [Fact]
        public void ShouldBeSetAsInactiveWhenEvaluatedAsEfficient()
        {
           Nonconformity nonconformity = new Nonconformity("Controlled materials stored without proper indication.");
           nonconformity.setAsEfficient();
           Assert.Equal(StatusEnum.Inactive, nonconformity.Status);
        }
    }
}