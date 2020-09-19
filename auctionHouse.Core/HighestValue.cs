using System.Linq;

namespace auctionHouse.Core
{
    public class HighestValue : IEvaluationModalities
    {
        public Bid Measure(Auction auction)
        {
            return auction.Binds
                .DefaultIfEmpty(new Bid(null, 0))
                .OrderBy(l => l.Value)
                .LastOrDefault();
        }
    }
}
