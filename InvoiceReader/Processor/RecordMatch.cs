using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceReader.Processor
{
    /// <summary>
    /// Match of record with found pdf file path
    /// </summary>
    public class RecordMatch
    {
        /// <summary>
        /// Line idx from csv - record
        /// </summary>
        public int RecordIdx { get; set; }
        /// <summary>
        /// Line from csv - record
        /// </summary>
        public string Record { get; set; }
        /// <summary>
        /// Path to matched pdf file
        /// </summary>
        public string PdfPath { get; set; } 
    }
}
