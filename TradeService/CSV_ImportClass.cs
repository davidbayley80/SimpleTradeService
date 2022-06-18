using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TradeService
{
    public class CSV_ImportClass
    {
        public List<TradeAttributes> CSV_Import_Method(string csv, char seperator)
        {

            var tradeList = new List<TradeAttributes>();

            using (StreamReader streamReader = new StreamReader(csv))
            {

                // var lineCount = LineCount(csv, seperator);
                var lineCount = System.IO.File.ReadLines(csv).Count();

                for (var i = 0; i < lineCount; i++)
                {
                    // Reades the current line in the streamReader
                    var line = streamReader.ReadLine();
                    // Splits the line based on the seperator defined 
                    var values = line.Split(seperator);

                    TradeAttributes trade = new TradeAttributes
                    {
                        TradeID = values[0],
                        Customer = values[1],
                        value = values[2]

                    };

                    tradeList.Add(trade);

                }
                return tradeList;
            }
        }

    }
}


