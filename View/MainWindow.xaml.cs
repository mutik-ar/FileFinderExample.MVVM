using ShellFileDialogs;
using System.ComponentModel;
using System.Windows;
using Interfaces.ViewModel;
using System.Globalization;
using System.Windows.Data;
using System.Timers;



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
        private System.Timers.Timer _timer;

        public MainWindow(IViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            if (_viewModel.RefreshMode.mode == Shared.Mode.Refresh)
            {
                SetTimer(_viewModel.RefreshMode.interval);
            }
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

        private void SetTimer(int interval)
        {
            // Create a timer with a two second interval.
            _timer = new System.Timers.Timer(interval);
            // Hook up the Elapsed event for the timer. 
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            //Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}", e.SignalTime);
            //       The Elapsed event was raised at 09:40:31.084
            _viewModel.RefreshActionCommand.Execute();
        }


    }

    [ValueConversion(typeof(bool), typeof(Visibility))]
    public sealed class BoolToVisibilityConverter : IValueConverter
    {
        public Visibility TrueValue { get; set; }
        public Visibility FalseValue { get; set; }
        public Visibility NullValue { get; set; }

        public BoolToVisibilityConverter()
        {
            // set defaults
            TrueValue = Visibility.Visible;
            FalseValue = Visibility.Hidden;
            NullValue = Visibility.Collapsed;
        }

        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null)
                return NullValue;
            return (bool)value ? TrueValue : FalseValue;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (Equals(value, TrueValue))
                return true;
            if (Equals(value, FalseValue))
                return false;
            return null;
        }
    }
}