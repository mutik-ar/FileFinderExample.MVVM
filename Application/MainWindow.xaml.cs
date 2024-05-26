using ShellFileDialogs;
using System.ComponentModel;
using System.Windows;



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

        private void ButtonSelectSearchDirectory_Click(object sender, RoutedEventArgs e)
        {
            string selectedDirectory = FolderBrowserDialog.ShowDialog(parentHWnd: IntPtr.Zero, title: "Выбрать папку...", initialDirectory: null);
            if (selectedDirectory != null)
            {
                string selectedPath = selectedDirectory;
                if (!selectedPath.EndsWith("\\"))
                {
                    selectedPath += "\\";
                }
                viewModel.UpdateSearchPathReadonlyTextBox(selectedPath);
            }

        }
    }
}