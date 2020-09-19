using auctionHouse.Core;
using Xunit;

namespace auctionHouse.Test
{
    public class BidCtor
    {
        [Fact]
        public void ArgumentExceptionGivenNegativeValue()
        {
            //Arranje
            var negativeValue = -100;

            //Assert
            Assert.Throws<System.ArgumentException>(
            //Act
                () => new Bid(null, negativeValue)
            );
        }

    }
}
