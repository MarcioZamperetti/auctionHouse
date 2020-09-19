using System.Collections.Generic;

namespace auctionHouse.Core
{
    public enum AuctionStatus
    {
        AuctionBeforePreaching,
        AuctionInProgress,
        AuctionFinished
    }

    public class Auction
    {
        private Client _lastClient;
        private IList<Bid> _bid;
        private IEvaluationModalities _evaluationMod;

        public IEnumerable<Bid> Binds => _bid;
        public string Car { get; }
        public Bid Winner { get; private set; }
        public AuctionStatus Status { get; private set; }

        public Auction(string car, IEvaluationModalities evaluationMod)
        {
            Car = car;
            _bid = new List<Bid>();
            Status = AuctionStatus.AuctionBeforePreaching;
            _evaluationMod = evaluationMod;
        }

        private bool NewBidIsAccepted(Client client, double value)
        {
            return (Status == AuctionStatus.AuctionInProgress)
                && (client != _lastClient);
        }

        public void ReceiveBid(Client client, double value)
        {
            if (NewBidIsAccepted(client, value))
            {
                _bid.Add(new Bid(client, value));
                _lastClient = client;
            }
        }

        public void StartsPreaching()
        {
            Status = AuctionStatus.AuctionInProgress;
        }

        public void EndsPreaching()
        {
            if (Status != AuctionStatus.AuctionInProgress)
            {
                throw new System.InvalidOperationException("It is not possible to finish the trading session without it having started.");
            }
            Winner = _evaluationMod.Measure(this);
            Status = AuctionStatus.AuctionFinished;
        }
    }
}
