using System;
using System.IO;
using System.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TradeService
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


