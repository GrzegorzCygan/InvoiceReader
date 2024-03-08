using InvoiceReader.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceReader.Processor
{
    /// <summary>
    /// Processor matching csv records with pdf files
    /// </summary>
    public class PdfProcessor : IPdfProcessor
    {
        private readonly IProcessorFactory _factory;
        private readonly string _csvFilePath;
        private readonly string _pdfFilesFolderPath;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="factory">Factory for file extractors</param>
        /// <param name="csvFilePath">path to csv file</param>
        /// <param name="pdfFilesFolderPath">Paths to pdf files</param>
        /// <exception cref="ArgumentNullException"></exception>
        public PdfProcessor(IProcessorFactory factory, string csvFilePath, string pdfFilesFolderPath)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _csvFilePath = csvFilePath ?? throw new ArgumentNullException(nameof(csvFilePath));
            _pdfFilesFolderPath = pdfFilesFolderPath ?? throw new ArgumentNullException(nameof(pdfFilesFolderPath));
        }
        /// <inheritdoc/>
        public List<RecordMatch> GetMatches()
        {
            List<RecordMatch> matches = new List<RecordMatch>();
            List<string[]> records = _factory.GetCSVReader(_csvFilePath).GetLines().ToList();

            // test each pdf file
            foreach (string path in _factory.GetPdfFinder(_pdfFilesFolderPath).GetPdfPaths())
            {
                string data = _factory.GetPdfExtractor(path).Extract();
                // test each record from csv file
                for (int i = 0; i < records.Count; i++)
                {
                    // all fields of record are present in csv record
                    if (records[i].All(r => data.Contains(r)))
                    { 
                        matches.Add(new RecordMatch() 
                        { 
                            RecordIdx = i, 
                            Record = string.Join(";", records[i]), 
                            PdfPath = path 
                        });
                    }
                }
            }
            // add not matched records
            for (int i = 0; i<records.Count; i++)
            {
                if (!matches.Any(m=> m.RecordIdx == i))
                {
                    matches.Add(new RecordMatch()
                    {
                        RecordIdx = i,
                        Record = string.Join(";", records[i]),
                    });
                }
            }
            matches.Sort((m1, m2) => m1.RecordIdx.CompareTo(m2.RecordIdx));
            return matches;
        }
    }
}
