using InvoiceReader.CSV;
using InvoiceReader.Extractor;
using InvoiceReader.Finder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceReader.Factory
{
    /// <summary>
    /// Factory that creates file Extractors
    /// </summary>
    public class ProcessorFactory : IProcessorFactory
    {
        /// <inheritdoc/> 
        public ICSVReader GetCSVReader(string path)
        {
            return new CSVReader(path, true, ';');
        }
        /// <inheritdoc/> 
        public IPdfExtractor GetPdfExtractor(string path)
        {
            return new PdfExtractor(path);
        }
        /// <inheritdoc/>
        public IPdfFinder GetPdfFinder(string path)
        {
            return new PdfFinder(path);
        }
    }
}
