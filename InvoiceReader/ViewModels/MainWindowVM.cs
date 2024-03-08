using InvoiceReader.Factory;
using InvoiceReader.Processor;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace InvoiceReader.ViewModels
{
    /// <summary>
    /// View model of main window
    /// </summary>
    public class MainWindowVM : BaseVM
    {
        private string _CSVFilePath;
        private string _PDFFolderPath;
        private ObservableCollection<RecordMatch> _matches = new ObservableCollection<RecordMatch>();
        private RecordMatch _selectedItem;

        /// <summary>
        /// Processor factory
        /// </summary>
        public IProcessorFactory Factory { get; set; }

        /// <summary>
        /// Path to CSV file with records
        /// </summary>
        public string CSVFilePath
        {
            get
            {
                return _CSVFilePath;
            }
            set
            {
                _CSVFilePath = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Path to folder with pdf files
        /// </summary>
        public string PDFFolderPath
        {
            get 
            { 
                return _PDFFolderPath; 
            }
            set
            {
                _PDFFolderPath = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Command to select csv file
        /// </summary>
        public ICommand SelectCSVFilePathCommand { get; }
        /// <summary>
        /// Command to select folder with pdf files
        /// </summary>
        public ICommand SelectPDFFolderPathCommand { get; }
        /// <summary>
        /// Command to match records from csv with pdf files
        /// </summary>
        public ICommand ProcessFilesCommand { get; }
        /// <summary>
        /// Collection with matched records and pdf files
        /// </summary>
        public ObservableCollection<RecordMatch> Matches
        {
            get
            {
                return _matches;
            }
            set
            {
                _matches = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Selected record
        /// </summary>
        public RecordMatch SelectedItem
        {
            get 
            { 
                return _selectedItem; 
            }
            set 
            { 
                _selectedItem = value; 
                OnPropertyChanged();
            }
        }
        public MainWindowVM()
        {
            SelectCSVFilePathCommand = new DelegatedCommand((o) => true, (o) => SelectCSVFilePathCommandExecute(o));
            SelectPDFFolderPathCommand = new DelegatedCommand((o) => true, (o) => SelectPDFFolderPathCommandExecute(o));
            ProcessFilesCommand = new DelegatedCommand((o) => true, (o) => ProcessFilesCommandExecute(o));
        }
        /// <summary>
        /// Choice of csv file
        /// </summary>
        /// <param name="sender"></param>
        private void SelectCSVFilePathCommandExecute(object _)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog
                {
                    Title = "Select CSV file",
                    DefaultExt = ".csv",
                    Filter = "CSV files (*.csv)|*.csv"
                };
                if (dlg.ShowDialog() == true)
                {
                    CSVFilePath = dlg.FileName;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Choice of folder with pdf files
        /// </summary>
        /// <param name="sender"></param>
        private void SelectPDFFolderPathCommandExecute(object _)
        {
            try
            {
                System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog()
                {
                    Description = "Select folder with PDF files",
                    SelectedPath = PDFFolderPath
                };
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    PDFFolderPath = dlg.SelectedPath;
                }
            }
            catch(Exception ex1) 
            { 
                MessageBox.Show(ex1.Message);
            }
        }
        /// <summary>
        /// Processing of matching files with records
        /// </summary>
        /// <param name="sender"></param>
        private void ProcessFilesCommandExecute(object _)
        {
            if (string.IsNullOrWhiteSpace(CSVFilePath))
            {
                MessageBox.Show("You should select a CSV file first!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(PDFFolderPath))
            {
                MessageBox.Show("You should select PDF files folder first!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                IPdfProcessor processor = new PdfProcessor(Factory, CSVFilePath, PDFFolderPath);
                List<RecordMatch> matches = processor.GetMatches();
                Matches = new ObservableCollection<RecordMatch>(matches);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
