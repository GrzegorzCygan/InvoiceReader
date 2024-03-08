using InvoiceReader;
using InvoiceReader.Factory;
using InvoiceReader.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceReaderUnitTests.ViewModels
{
    [TestClass]
    public class MainWindowVMUnitTests
    {
        [TestMethod]
        public void MainWindowVMTestCtor()
        {
            MainWindowVM vm = new MainWindowVM();
            Assert.IsNotNull(vm.SelectCSVFilePathCommand, "Command should be assigned");
            Assert.IsNotNull(vm.SelectPDFFolderPathCommand, "Command should be assigned");
            Assert.IsNotNull(vm.ProcessFilesCommand, "Command should be assigned");

            Assert.IsNull(vm.CSVFilePath, "Path should not be assigned");
            Assert.IsNull(vm.PDFFolderPath, "Path should not be assigned");
        }
        [TestMethod]
        public void MainWindowVMTestProcessOk() 
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Data");
            string csvPath = Path.Combine(path, "Dane do wyszukania.csv");
            string pdfPath1 = Path.Combine(path, "Faktura1.pdf");
            string pdfPath2 = Path.Combine(path, "Faktura2.pdf");

            MainWindowVM vm = new MainWindowVM();
            vm.CSVFilePath = csvPath;
            vm.PDFFolderPath = path;
            vm.Factory = new ProcessorFactory();

            vm.ProcessFilesCommand.Execute(null);
            Assert.AreEqual(2, vm.Matches.Count, "Should be 2 matched records");
            Assert.AreEqual(2, vm.Matches.Where(m => !string.IsNullOrEmpty(m.PdfPath)).Count(), "Should be 2 matched files");
            Assert.AreEqual(pdfPath1, vm.Matches[0].PdfPath, "Wrong matched file");
            Assert.AreEqual(pdfPath2, vm.Matches[1].PdfPath, "Wrong matched file");

        }
        [TestInitialize]
        public void TestInitialize()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(Globals.SyncfusionKey);
        }
    }
}
