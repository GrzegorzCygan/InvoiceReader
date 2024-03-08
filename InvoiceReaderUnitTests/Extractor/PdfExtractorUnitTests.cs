using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using InvoiceReader.Extractor;
using System.Reflection;
using InvoiceReader;

namespace InvoiceReaderUnitTests.Extractor
{
    [TestClass]
    public class PdfExtractorUnitTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Should throw exception on null path")]
        public void TestPdfExctractorCtorThrowsOnNullPath()
        {
            _ = new PdfExtractor(null);
        }
        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException), "Should throw exception on wrong path")]
        public void TEstPdfExtractorOnWrongPath()
        {
            PdfExtractor extractor = new PdfExtractor("c:\\notexistingpath.pdf");
            _ = extractor.Extract();
        }
        [TestMethod]
        public void TestPdfExtractorExtract() 
        {
            string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            PdfExtractor extractor = new PdfExtractor(Path.Combine(assemblyPath, "Data", "Faktura1.pdf"));
            string lines = extractor.Extract();
            Assert.IsTrue(lines.Contains("2016/08/C"), "Invoice should contain 2016/08/C");
        }
        [TestInitialize] 
        public void TestInitialize()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(Globals.SyncfusionKey);
        }
    }
}
