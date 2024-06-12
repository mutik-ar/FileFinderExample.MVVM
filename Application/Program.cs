using Interfaces.Model;
using Interfaces.ViewModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;

namespace FileFinderExample
{
    public partial class Program
    {
        public IModel? Model { get; set; }
        public IViewModel ViewModel { get; set; }

        [STAThread]
        public static void Main()
        {
            Program program = new Program();
            program.Composite();
        }

        public void Composite()
        {
            var conventions = new RegistrationBuilder();
            conventions.ForTypesDerivedFrom<IModel>().Export<IModel>();
            conventions.ForType<Program>().ImportProperty<IModel>(p => p.Model);
            conventions.ForTypesDerivedFrom<IViewModel>().Export<IViewModel>();
            conventions.ForType<Program>().ImportProperty<IViewModel>(p => p.ViewModel);
            var catalog = new DirectoryCatalog(AppDomain.CurrentDomain.BaseDirectory, conventions);
            CompositionService service = catalog.CreateCompositionService();
            service.SatisfyImportsOnce(this, conventions);

            View.MainWindow mainWindow = new(ViewModel);
            mainWindow.ShowDialog();

        }
    }

}
