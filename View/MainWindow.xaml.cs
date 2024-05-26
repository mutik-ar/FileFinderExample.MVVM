using ShellFileDialogs;
using System.ComponentModel;
using System.Windows;
using Interfaces.ViewModel;



namespace View
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
        IViewModel _viewModel;
        //IViewModel viewModel = new ViewModel.ViewModel(new Model.Model());
        public MainWindow(IViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
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
                _viewModel.UpdateSearchPathReadonlyTextBox(selectedPath);
            }

        }
    }
}