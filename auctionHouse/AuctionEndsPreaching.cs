using auctionHouse.Core;
using Xunit;

namespace auctionHouse.Test
{
    public class AuctionEndsPreaching
    {
        [Theory]
        [InlineData(1200000, 1250000, new double[] { 800000, 1150000, 1400000, 1250000 })]
        public void ReturnsHighestNearestValueGivenAuctionInThisMode(
            double valueDestination,
            double expectedAmount,
            double[] offers)
        {
            //Arranje
            IEvaluationModalities modality =
                new NearestSuperiorBid(valueDestination);
            var auction = new Auction("Corvette CR7", modality);
            var buyer = new Client("Marcio", auction);
            var buyer2 = new Client("Maria", auction);

            auction.StartsPreaching();
            for (int i = 0; i < offers.Length; i++)
            {
                if ((i % 2 == 0))
                {
                    auction.ReceiveBid(buyer, offers[i]);
                }
                else
                {
                    auction.ReceiveBid(buyer2, offers[i]);
                }
            }

            //Act
            auction.EndsPreaching();

            //Assert
            Assert.Equal(expectedAmount, auction.Winner.Value);

        }

        [Theory]
        [InlineData(1200000, new double[] { 800000, 900000, 1000000, 1200000 })]
        [InlineData(1000000, new double[] { 800000, 900000, 1000000, 990000 })]
        [InlineData(800000, new double[] { 800000 })]
        public void ReturnsHighestValueGivenAuctionWithAtOneBid(
            double expectedAmount,
            double[] offers)
        {
            //Arranje
            var modality = new HighestValue();
            var auction = new Auction("Corvette CR7", modality);
            var buyer = new Client("Marcio", auction);
            var buyer2 = new Client("Maria", auction);
            auction.StartsPreaching();
            for (int i = 0; i < offers.Length; i++)
            {
                var value = offers[i];
                if ((i % 2) == 0)
                {
                    auction.ReceiveBid(buyer, value);
                }
                else
                {
                    auction.ReceiveBid(buyer2, value);
                }
            }

            //Act
            auction.EndsPreaching();

            //Assert
            var obtainedAmount = auction.Winner.Value;
            Assert.Equal(expectedAmount, obtainedAmount);

        }

        [Fact]
        public void InvalidOperationExceptionWhenPreachingNotStarted()
        {
            //Arranje
            var modality = new HighestValue();
            var auction = new Auction("Corvette CR7", modality);

            //Assert
            var exceptionObtained = Assert.Throws<System.InvalidOperationException>(
                //Act
                () => auction.EndsPreaching()
            );

            var expectedMessage = "It is not possible to finish the trading session without it having started.";
            Assert.Equal(expectedMessage, exceptionObtained.Message);
        }

        [Fact]
        public void ReturnsZeroGivenAuctionWithoutBids()
        {
            //Arranje
            var modality = new HighestValue();
            var auction = new Auction("Corvette CR7", modality);
            auction.StartsPreaching();

            //Act
            auction.EndsPreaching();

            //Assert
            var expectedAmount = 0;
            var obtainedAmount = auction.Winner.Value;

            Assert.Equal(expectedAmount, obtainedAmount);
        }
    }
}
