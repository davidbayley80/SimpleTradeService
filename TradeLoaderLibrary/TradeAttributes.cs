using System.Globalization;

namespace TradeLoaderLibrary
{
    public class TradeAttributes
    {
        public string _tradeID;
        public string _customer;
        public double _price;
        public string _counterParty;
        public string _portfolio;
        public DateTime _timeStamp;
        
       //  CultureInfo provider = CultureInfo.InvariantCulture;
       // string dateFormat = "MM/dd/yyyy hh:mm:ss tt";

        public TradeAttributes(string TradeID, string Customer, string Price, string CountryParty, string Portfolio, string TimeStamp)
        {
            _tradeID = TradeID;
            _customer = Customer;
            _price = double.Parse(Price);
            _counterParty = CountryParty;
            _portfolio = Portfolio;

            string TestTimeStamp = "06/17/2022 11:30:00 AM"; // Replace with your actual data
            string dateFormat = "MM/dd/yyyy hh:mm:ss tt";
            CultureInfo provider = CultureInfo.InvariantCulture;

            bool result = DateTime.TryParseExact(TestTimeStamp, dateFormat, provider, DateTimeStyles.None, out _timeStamp);

            
            
            
        }
    }
}


