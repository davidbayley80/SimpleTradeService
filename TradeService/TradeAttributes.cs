namespace TradeService
{
    public class TradeAttributes
    {
        public string _tradeID;
        public string _customer;
        public double _price;
        public string _counterParty;
        public string _portfolio;
        public DateTime _timeStamp;

        public TradeAttributes(string TradeID, string Customer, string Price, string CountryParty, string Portfolio, string TimeStamp)
        {
            _tradeID = TradeID;
            _customer = Customer;
            _price = double.Parse(Price);
            _counterParty = CountryParty;
            _portfolio = Portfolio;
            _timeStamp = DateTime.Parse(TimeStamp);

        }
    }
}


