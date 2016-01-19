using Xunit;

namespace SampleTests
{
    public class Given_some_tests
    {
        [Fact]
        public void When_a_test_passes_it_is_reported_as_passing()
        {
            Assert.Equal(true, true);
        }
        
        [Fact]
        public void When_a_test_fails_it_is_reported_as_failing()
        {
            Assert.Equal(true, false);
        }
    }
}
