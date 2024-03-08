using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceReader.Processor
{
    /// <summary>
    /// Processor that matches csv row with pdf files
    /// </summary>
    public interface IPdfProcessor
    {
        /// <summary>
        /// Get matched csv rows with pdf files
        /// </summary>
        /// <returns>Collection of RecordMatch</returns>
        List<RecordMatch> GetMatches();
    }
}
