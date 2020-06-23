using NonconformityControl.Models;
using Xunit;

namespace NonconformityControl.Tests.Models
{
    public class ActionTest
    {
        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("      ")]
        public void ActionDescriptionShouldBeRequired(string description)
        {
            Action action = new Action(1, description);
            Assert.False(action.isValid());
        }
    }
}