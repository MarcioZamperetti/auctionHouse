using System.Linq;

namespace auctionHouse.Core
{
    public class NearestSuperiorBid : IEvaluationModalities
    {
        public double ValueDestination { get; }

        public NearestSuperiorBid(double valueDestination)
        {
            ValueDestination = valueDestination;
        }

        public Bid Measure(Auction auction)
        {
            return auction.Binds
                .DefaultIfEmpty(new Bid(null, 0))
                .Where(l => l.Value > ValueDestination)
                .OrderBy(l => l.Value)
                .FirstOrDefault();
        }
    }
}
