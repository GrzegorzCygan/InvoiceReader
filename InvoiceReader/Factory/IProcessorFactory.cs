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
    /// Interface to factory serving file extractors
    /// </summary>
    public interface IProcessorFactory
    {
        /// <summary>
        /// Creates CSVReader
        /// </summary>
        /// <param name="path">Path to csv file</param>
        /// <returns>CSVReader</returns>
        ICSVReader GetCSVReader(string path);
        /// <summary>
        /// Creates PdfExctractor
        /// </summary>
        /// <param name="path">Path to pdf file</param>
        /// <returns>PdfExctractor</returns>
        IPdfExtractor GetPdfExtractor(string path);
        /// <summary>
        /// Creates PdfFinder
        /// </summary>
        /// <param name="path">Path to folder with pdf files</param>
        /// <returns>PdfFinder</returns>
        IPdfFinder GetPdfFinder(string path);
    }
}
