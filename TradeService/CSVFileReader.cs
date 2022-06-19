using System;
namespace TradeService
{
	public class CSVFileReader : ICSVFileReader
	{
		private readonly string _filePath;

		public CSVFileReader(string filePath) => _filePath = filePath;

		public IEnumerable<TradeAttributes> Parse()
        {
			foreach (var line in File.ReadLines(_filePath).Skip(1))
			{
				if (string.IsNullOrWhiteSpace(line)) continue;
				var lineParts = line.Split(',');
				if (lineParts.Length < 3) continue;

				// Yield returns one row at at time to IEnumerable 
				yield return new TradeAttributes(lineParts[0], lineParts[1], lineParts[2], lineParts[3], lineParts[4], lineParts[5]);
			}
        }
	}
}

