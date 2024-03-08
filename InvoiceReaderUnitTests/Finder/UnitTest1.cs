using InvoiceReader.Factory;
using InvoiceReader.Finder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace InvoiceReaderUnitTests.Finder
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PdfFinderUnitTest_ctor_throwsOnNullPath()
        {
            _ = new PdfFinder(null);
        }
        [TestMethod]
        public void PdfFinderUnitTest_FindsOK()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Data");
            IPdfFinder finder = new PdfFinder(path);
            List<string> paths = finder.GetPdfPaths();
            Assert.AreEqual(2, paths.Count, "Wrong number of finded pdf files");
            Assert.AreEqual(Path.Combine(path, "Faktura1.pdf"), paths[0]);
            Assert.AreEqual(Path.Combine(path, "Faktura2.pdf"), paths[1]);
        }
    }
}
