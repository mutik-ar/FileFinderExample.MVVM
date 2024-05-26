namespace FileFinderExample
{
    public partial class Program
    {
        [STAThread]
        public static void Main()
        {
            Model.Model model = new();
            ViewModel.ViewModel viewModel = new(model);
            View.MainWindow mainWindow = new(viewModel);
            mainWindow.ShowDialog();
        }
    }

}
