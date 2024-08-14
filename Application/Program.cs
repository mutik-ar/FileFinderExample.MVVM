using Interfaces.Model;
using Interfaces.ViewModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.Reflection;
using System.Windows;
using Shared;

namespace FileFinderExample
{
    public partial class Program
    {

        #region Fieled
        #endregion

        #region Properties
        public IModel Model { get; set; }
        public IViewModel ViewModel { get; set; }

        //[Export(typeof(RefreshMode))]
        public RefreshMode refreshMode { get; set; }

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
            conventions.ForType<Program>().ImportProperty<IModel>(p => p.Model);
            conventions.ForTypesDerivedFrom<IViewModel>().Export<IViewModel>();
            conventions.ForType<Program>().ImportProperty<IViewModel>(p => p.ViewModel);
            RefreshMode.Interval = 500;
            RefreshMode.Mode = Mode.Refresh;
            conventions.ForType<RefreshMode>().Export<RefreshMode>();
            conventions.ForType<Program>().ImportProperty<RefreshMode>(p => p.refreshMode);


            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new DirectoryCatalog(AppDomain.CurrentDomain.BaseDirectory, conventions));
            CompositionService service = catalog.CreateCompositionService();
            service.SatisfyImportsOnce(this, conventions);

            View.MainWindow mainWindow = new(ViewModel);
            mainWindow.ShowDialog();

        }
    }

}
