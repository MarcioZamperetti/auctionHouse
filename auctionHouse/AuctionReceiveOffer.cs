using auctionHouse.Core;
using Xunit;
using System.Linq;

namespace auctionHouse.Test
{
    public class LeilaoRecebeOferta
    {
        [Fact]
        public void DontAcceptNextBidWhenSameClientPerformedLastBid()
        {
            //Arranje
            var modality = new HighestValue();
            var auction = new Auction("Corvette CR7", modality);
            var buyer = new Client("Marcio", auction);
            auction.StartsPreaching();
            auction.ReceiveBid(buyer, 800);

            //Act
            auction.ReceiveBid(buyer, 1000);

            //Assert
            var expectedAmount = 1;
            var obtainedAmount = auction.Binds.Count();
            Assert.Equal(expectedAmount, obtainedAmount);
        }

        [Theory]
        [InlineData(4, new double[] { 1000000, 1200000, 1400000, 1300000 })]
        [InlineData(2, new double[] { 800000, 900000 })]
        public void DoesNotAllowNewBidsGivenAuctionEnded(
            int expectedAmount, double[] offers)
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
            auction.EndsPreaching();

            //Act
            auction.ReceiveBid(buyer, 1000);

            //Assert
            var obtainedAmount = auction.Binds.Count();
            Assert.Equal(expectedAmount, obtainedAmount);
        }
    }
}
