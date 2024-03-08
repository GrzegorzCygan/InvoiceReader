using InvoiceReader.CSV;
using InvoiceReader.Extractor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceReaderUnitTests.CSV
{
    [TestClass]
    public class CSVReaderUnitTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Should throw on empty path")]
        public void CSVReaderUnitTests_Null()
        {
            _ = new CSVReader(null, true, ';');
        }
        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException), "Shold throw on wrong path")]
        public void CSVReaderUnitTests_WrongPath()
        {
            CSVReader reader = new CSVReader("c:\\wrongpath.csv", true, ';');
            _ = reader.GetLines().ToList();
        }
        [TestMethod]
        public void CSVReaderUnitTests_ReadsOK()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Data", "Dane do wyszukania.csv");
            CSVReader reader = new CSVReader(path, true, ';');
            List<string[]> lines = reader.GetLines().ToList();

            Assert.AreEqual(2, lines.Count);
            Assert.AreEqual(lines[0][0], "2016/08/C");
            Assert.AreEqual(lines[0][1], "30.06.2016");
            Assert.AreEqual(lines[0][2], "22 960,00");
            Assert.AreEqual(lines[1][0], "2016/09/C");
            Assert.AreEqual(lines[1][1], "30.08.2016");
            Assert.AreEqual(lines[1][2], "1 960,00");
        }
        [TestMethod]
        public void CSVReaderUnitTests_ReadsOKWithHeader()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Data", "Dane do wyszukania.csv");
            CSVReader reader = new CSVReader(path, false, ';');
            List<string[]> lines = reader.GetLines().ToList();

            Assert.AreEqual(3, lines.Count);
            Assert.AreEqual(lines[0][0], "Numer faktury");
            Assert.AreEqual(lines[0][1], "Data");
            Assert.AreEqual(lines[0][2], "Kwota");
            Assert.AreEqual(lines[1][0], "2016/08/C");
            Assert.AreEqual(lines[1][1], "30.06.2016");
            Assert.AreEqual(lines[1][2], "22 960,00");
            Assert.AreEqual(lines[2][0], "2016/09/C");
            Assert.AreEqual(lines[2][1], "30.08.2016");
            Assert.AreEqual(lines[2][2], "1 960,00");
        }
    }
}
