using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceReader.Extractor
{
    /// <summary>
    /// Interface for extracting text from pdf
    /// </summary>
    public interface IPdfExtractor
    {
        /// <summary>
        /// Returns lines of text extracted from pdf
        /// </summary>
        /// <returns>Row text</returns>
        string Extract();
    }
}
