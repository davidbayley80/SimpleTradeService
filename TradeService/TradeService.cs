//using System.Collections;

//namespace TradeService
//{
//    // TradeService Takes the Trade details from a csv and puts them on a queue
//    // This can be used to process data further or persist to downstream applications
//    public class TradeService
//    {
//        private CSV_Import newCSV_ImportClass = new CSV_Import();
//        Queue<TradeAttributes> tradeQueue = new Queue<TradeAttributes>();

//        public IEnumerable GetTrades(string csv, char seperator)
//        {

//            var tradeList = newCSV_ImportClass.CSV_Import_Method(csv, seperator);
//            // Place Trades on a Queue
//            Enqueue(tradeList);
//            return null;
//        }

//        public void Enqueue(List<TradeAttributes> tradeList)
//        {
//            foreach (var trade in tradeList)
//            {
//                tradeQueue.Enqueue(trade);
//            }
//        }
//    }
//}


