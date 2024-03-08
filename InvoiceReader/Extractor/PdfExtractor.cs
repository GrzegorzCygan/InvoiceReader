using Syncfusion.Pdf;
using Syncfusion.Windows.PdfViewer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace InvoiceReader.Extractor
{
    /// <summary>
    /// Extractor of text from pdf
    /// </summary>
    public class PdfExtractor : IPdfExtractor
    {
        private readonly string _pdfFilePath;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pdfFilePath">Path to pdf file</param>
        public PdfExtractor(string pdfFilePath)
        {
            _pdfFilePath = pdfFilePath ?? throw new ArgumentNullException(nameof(pdfFilePath));
        }
        /// <inheritdoc />
        public string Extract()
        {
            if (!File.Exists(_pdfFilePath))
            {
                throw new FileNotFoundException(_pdfFilePath);
            }

            PdfDocumentView viewer = new PdfDocumentView();
            viewer.Load(_pdfFilePath);

            string extractedText = viewer.ExtractText(0, out TextLines _);

            return extractedText;
        }
    }
}
