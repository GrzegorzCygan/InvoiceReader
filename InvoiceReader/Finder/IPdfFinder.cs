using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceReader.Finder
{
    /// <summary>
    /// Finds pdf files in folder path
    /// </summary>
    public interface IPdfFinder
    {
        /// <summary>
        /// Finds paths of pdf files
        /// </summary>
        /// <returns>List of paths to pdf files</returns>
        List<string> GetPdfPaths();
    }
}
