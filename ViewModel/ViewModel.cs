using System.ComponentModel;
using System.Runtime.CompilerServices;
using Interfaces.ViewModel;
using Interfaces.Model;


namespace ViewModel
{
    public class ViewModel: INotifyPropertyChanged, IViewModel
    {
        #region Fields

        private IModel _model;

        private vmComboBox<DriveInfoItem> _drives;
        private vmTextBox _searchPath;
        private vmTextBox _filePart;

        #endregion

        #region Properties

        public vmComboBox<DriveInfoItem> Drives
        {
            get
            {
                return _drives;
            }

        }

        public vmTextBox SearchPath 
        { 
            get
            {
                return _searchPath;
            }
        }

        public vmTextBox FilePart
        {
            get
            {
                return _filePart;
            }
        }


        #endregion

        #region Constructor

        public ViewModel(IModel model)
        {
            _model = model;
            _drives = new vmComboBox<DriveInfoItem>(_model.DrivesList);
            _drives.PropertyChanged += Drives_Changed;
            _searchPath = new();
            _searchPath.PropertyChanged += SearchPath_Changed;
            _filePart = new();
            _filePart.PropertyChanged += FilePart_Changed;
        }


        //public ViewModel()
        //{
        //    _model = new Model.Model();
        //}

        #endregion

        #region Public Methods

        public void UpdateSearchPathReadonlyTextBox(string searchPath)
        {
            //if (!searchPath.Equals(SearchPath.Text) && FileSearchInfoHolder.FilesTotalCount > 0)
            //{
            //    FileSearchInfoHolder.FilesTotalCount = 0;
            //}
            SelectDriveBySearchPath(searchPath);
            SearchPath.Text = searchPath;
       }

        /// <summary>
        /// Метод выбирает один из доступных в системе дисков в выпадающем списке по заданному пути поиска файлов.
        /// Находит в начале пути поиска <paramref name="searchPath"/> букву диска и выберет его в выпадающем списке.  
        /// </summary>
        /// <param name="searchPath">путь поиска файлов</param>
        public void SelectDriveBySearchPath(string searchPath)
        {
            int commaSlashPosition = searchPath.IndexOf(":\\");
            if (commaSlashPosition >= 0)
            {
                string driveLetterFromPath = searchPath.Substring(0, commaSlashPosition + 2);

                foreach (var item in Drives.List)
                {
                    if (item is DriveInfoItem driveInfoItem)
                    {
                        if (driveInfoItem.DriveName.Equals(driveLetterFromPath))
                        {
                            Drives.SelectedItem = item;
                            break;
                        }
                    }
                }
            }
            else
            {
                //MessageBox.Show("Ошибка: невозможно найти диск, соответствующий выбранному пути", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        #endregion


        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChangedEventHandler? handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void Drives_Changed(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Drives.List))
            {
            }
            if (e.PropertyName == nameof(Drives.SelectedItem))
            {
                var selectedItem = Drives.SelectedItem;
                if (selectedItem is DriveInfoItem driveInfoItem)
                {
                    string selectedPath = driveInfoItem.DriveName;
                    SearchPath.Text = selectedPath;
                    //FileSearchInfoHolder.SearchDirectory = selectedPath;
                    //UpdateSearchPathReadonlyTextBox(selectedPath);
                }


            }
            if (e.PropertyName == nameof(Drives.SelectedIndex))
            {

            }
        }

        private void SearchPath_Changed(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SearchPath.Text))
            {

            }
        }
        private void FilePart_Changed(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(FilePart.Text))
            {

            }
        }



        #endregion
    }
}
