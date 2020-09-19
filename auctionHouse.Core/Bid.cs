namespace auctionHouse.Core
{
    public class Bid
    {
        public Client Client { get; }
        public double Value { get; }

        public Bid(Client client, double value)
        {
            if (value < 0)
            {
                throw new System.ArgumentException("Bid amount must be equal to or greater than zero.");
            }
            Client = client;
            Value = value;
        }
    }
}
