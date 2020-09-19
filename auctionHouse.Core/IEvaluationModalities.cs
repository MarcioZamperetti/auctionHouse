namespace auctionHouse.Core
{
    public interface IEvaluationModalities
    {
        Bid Measure(Auction auction);
    }
}
