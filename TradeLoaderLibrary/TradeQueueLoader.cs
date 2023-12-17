using System.Collections.Concurrent;
using TradeLoaderLibrary;

namespace TradeLoaderLibrary
{
    public class TradeQueueLoader
    {
        
        private readonly string _tradeFile;
        private readonly BlockingCollection<TradeAttributes> _items; 

        public TradeQueueLoader(string tradeFile)
        {
            _tradeFile = tradeFile;
            _items = new BlockingCollection<TradeAttributes>();
            
            

        }

        public async Task LoadAsync()
        {
            await Task.Run(LoadRecords);
            
        }
        

        public void LoadRecords()
        {
            foreach (var record in new CSVFileReader(_tradeFile).Parse())
            {
                _items.Add(record);
            }
            
            _items.CompleteAdding();
        }

        public BlockingCollection<TradeAttributes> TradeRecords => _items;

    }
}


