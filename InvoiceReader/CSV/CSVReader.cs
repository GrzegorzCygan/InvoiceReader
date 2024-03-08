using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceReader.CSV
{
    /// <summary>
    /// Reads csv file
    /// </summary>
    public class CSVReader : ICSVReader
    {
        private readonly string _csvFilePath;
        private readonly bool _skipHeader;
        private readonly char _separator;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="csvFilePath">Path to csv file</param>
        /// <param name="skipHeader">Does CSV contains header line</param>
        /// <param name="separator">Fields separator</param>
        /// <exception cref="ArgumentNullException"></exception>
        public CSVReader(string csvFilePath, bool skipHeader, char separator)
        {
            _csvFilePath = csvFilePath ?? throw new ArgumentNullException(nameof(csvFilePath));
            _skipHeader = skipHeader;
            _separator = separator;
        }
        /// <inheritdoc/>
        public IEnumerable<string[]> GetLines()
        {
            if (!File.Exists(_csvFilePath))
            {
                throw new FileNotFoundException(_csvFilePath);
            }
            foreach(string line in File.ReadAllLines(_csvFilePath).Skip(_skipHeader?1:0))
            {
                List<string> fields = new List<string>();
                foreach (string field in line.Split(new char[] { _separator }, StringSplitOptions.RemoveEmptyEntries))
                {
                    fields.Add(field.Trim(new char[] {' '}));
                }
                yield return fields.ToArray();
            }
        }
    }
}
