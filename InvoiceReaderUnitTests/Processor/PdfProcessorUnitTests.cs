using InvoiceReader;
using InvoiceReader.Extractor;
using InvoiceReader.Factory;
using InvoiceReader.Processor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceReaderUnitTests.Processor
{
    [TestClass]
    public class PdfProcessorUnitTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Should throw exception")]
        public void ProcessorUnitTest_ctor_nullFactory()
        {
            _ = new PdfProcessor(null, "path", "path");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Should throw exception")]
        public void ProcessorUnitTest_ctor_nullCsvPath()
        {
            _ = new PdfProcessor(new ProcessorFactory(), null, "path");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Should throw exception")]
        public void ProcessorUnitTest_ctor_nullPdfPaths()
        {
            _ = new PdfProcessor(new ProcessorFactory(), "path", null);
        }
        [TestMethod]
        public void ProcessorUnitTest_ProcessOk()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Data");
            string csvPath = Path.Combine(path, "Dane do wyszukania.csv");
            string pdfPath1 = Path.Combine(path, "Faktura1.pdf");
            string pdfPath2 = Path.Combine(path, "Faktura2.pdf");

            PdfProcessor processor = new PdfProcessor(new ProcessorFactory(), csvPath, path);
            List<RecordMatch> matches = processor.GetMatches().ToList();
            Assert.AreEqual(2, matches.Count, "Should be 2 matches");
            Assert.AreEqual("2016/08/C;30.06.2016;22 960,00", matches[0].Record);
            Assert.AreEqual(0, matches[0].RecordIdx, "Wrong record idx");
            Assert.AreEqual(pdfPath1, matches[0].PdfPath, "Wrong pdf");
            Assert.AreEqual("2016/09/C;30.08.2016;1 960,00", matches[1].Record);
            Assert.AreEqual(1, matches[1].RecordIdx, "Wrong record idx");
            Assert.AreEqual(pdfPath2, matches[1].PdfPath, "Wrong pdf");
        }
        [TestInitialize]
        public void TestInitialize()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(Globals.SyncfusionKey);
        }
    }
}
