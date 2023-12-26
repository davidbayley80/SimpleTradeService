using System.Collections.Concurrent;
using TradeLoaderLibrary;

namespace TradeLoaderLibrary
{
    public class TradeQueueLoader : ITradeQueueLoader
    {
        private readonly ICSVFileReader _csvFileReader;
        private readonly BlockingCollection<TradeAttributes> _items; 

        public TradeQueueLoader(ICSVFileReader csvFileReader)
        {
            _csvFileReader = csvFileReader;
            _items = new BlockingCollection<TradeAttributes>();
        }
        

        public async Task LoadAsync()
        {
            await Task.Run(LoadRecords);
            
        }

        public void LoadRecords()
        {
            foreach (var record in _csvFileReader.Parse())
            {
                _items.Add(record);
            }
            
            _items.CompleteAdding();
        }

        public BlockingCollection<TradeAttributes> TradeRecords => _items;

    }
}


