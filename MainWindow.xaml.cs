using ShellFileDialogs;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using FileFinderExample.ViewModel;
using FileFinderExample.Model;

namespace FileFinderExample
{


    public struct FileProperty
    {
        public string Path { get; set; }

        public string Size { get; set; }

    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BackgroundWorker BackgroundWorkerEstimateSearchTime = new();
        BackgroundWorker BackgroundWorkerSearchFiles = new();
        public ViewModel.ViewModel viewModel = new ViewModel.ViewModel(new Model.Model());
        public MainWindow()
        {
            InitializeComponent();
            DataContext = viewModel;
        }


    }
}