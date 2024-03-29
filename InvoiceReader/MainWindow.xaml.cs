﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using InvoiceReader.Factory;
using InvoiceReader.ViewModels;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Interactive;
using Syncfusion.Windows.PdfViewer;

namespace InvoiceReader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Loading preview of selected pdf file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (this.DataContext is MainWindowVM model && model.SelectedItem != null)
                {
                    if (string.IsNullOrWhiteSpace(model.SelectedItem.PdfPath))
                    {
                        pdfViewer.Unload();
                    }
                    else
                    {
                        pdfViewer.Load(model.SelectedItem.PdfPath);
                        pdfViewer.ZoomTo(75);
                    }
                }
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);            
            }
        }
    }
}
