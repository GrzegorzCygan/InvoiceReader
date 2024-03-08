using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceReader.CSV
{
    /// <summary>
    /// Reads csv file
    /// </summary>
    public interface ICSVReader
    {
        /// <summary>
        /// /// Enumerates lines of csv file as array of strings
        /// Each line of file is array of strings
        /// </summary>
        /// <returns>Collection of lines of csv file each as array of strings representing fields.</returns>
        IEnumerable<string[]> GetLines();
    }
}
