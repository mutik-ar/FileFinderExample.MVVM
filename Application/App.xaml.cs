using System.Windows;
using View;


namespace FileFinderExample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [STAThread]
        public static void Main()
        {
            Model.Model model = new();
            ViewModel.ViewModel viewModel = new(model);
            MainWindow mainWindow = new(viewModel);
            mainWindow.ShowDialog();
        }
    }

}
