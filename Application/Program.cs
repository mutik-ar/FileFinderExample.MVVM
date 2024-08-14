using Interfaces.Model;
using Interfaces.ViewModel;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.Windows;
using Shared;

namespace FileFinderExample
{
    public class ParametrsForViewModel
    {
        public RefreshModes RefreshMode { get; } = RefreshModes.Refresh; // Events, Timer, Refresh
        public int RefreshInterval { get; } = 200; // мсек
    }
    public partial class Program
    {

        #region Fieled
        #endregion

        #region Properties
        #endregion


        [STAThread] // Вызывающим потоком должен быть STA, поскольку этого требуют большинство компонентов UI.
        public static void Main()
        {
            try
            {
                Program program = new Program();
                program.Composite();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Composite()
        {
            var conventions = new RegistrationBuilder();
            conventions.ForTypesDerivedFrom<IModel>().Export<IModel>();
            conventions.ForTypesDerivedFrom<IViewModel>().Export<IViewModel>();
            conventions.ForType<ParametrsForViewModel>().ExportProperties(p => p.Name == "RefreshInterval");
            conventions.ForType<ParametrsForViewModel>().ExportProperties(p => p.Name == "RefreshMode");

            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new DirectoryCatalog(AppDomain.CurrentDomain.BaseDirectory, conventions));

            CompositionContainer container = new CompositionContainer(catalog);
            IViewModel viewModel = container.GetExportedValue<IViewModel>();

            View.MainWindow mainWindow = new(viewModel);
            mainWindow.ShowDialog();

        }
    }

}
