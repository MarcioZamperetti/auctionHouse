namespace auctionHouse.Core
{
    public class Client
    {
        public string Name { get; }
        public Auction Auction { get; }

        public Client(string name, Auction auction)
        {
            Name = name;
            Auction = auction;
        }
    }
}
