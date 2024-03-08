using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceReader.Finder
{
    /// <summary>
    /// Finds pdf files in folder path
    /// </summary>
    public class PdfFinder : IPdfFinder
    {
        private readonly string _folderPath;

        public PdfFinder(string folderPath)
        {
            _folderPath = folderPath ?? throw new ArgumentNullException(nameof(folderPath));
        }
        /// <inheritdoc/>
        public List<string> GetPdfPaths()
        {
            DirectoryInfo directory = new DirectoryInfo(_folderPath);
            return directory.EnumerateFiles().
                Where(fi => Path.GetExtension(fi.FullName).ToLower() == ".pdf").
                Select(fi => fi.FullName).
                ToList();
        }
    }
}
