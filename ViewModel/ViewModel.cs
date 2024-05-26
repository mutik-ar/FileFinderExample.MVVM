using System.ComponentModel;
using Interfaces.ViewModel;
using Interfaces.Model;


namespace ViewModel
{
    public class ViewModel: IViewModel
    {
        #region Fields

        private IModel _model;

        private vmComboBox<DriveInfoItem> _drives;
        private vmTextBox _searchPath;
        private vmTextBox _fileNameMask;
        private vmButton _startSearch;
        private string _contentStopSearch = "Прервать";
        private string _contentStartSearch = "Начать поиск";

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

        public vmTextBox FileNameMask
        {
            get
            {
                return _fileNameMask;
            }
        }

        public vmButton StartSearch
        {
            get
            {
                return _startSearch;
            }
        }


        #endregion

        #region Constructor

        public ViewModel(IModel model)
        {
            _model = model;
            _drives = new vmComboBox<DriveInfoItem>(_model.DrivesList);
            _drives.PropertyChanged += Drives_Changed;
            _model.PropertyChanged += Model_Changed;
            _searchPath = new();
            _fileNameMask = new();
            _startSearch = new() { Content = _contentStartSearch } ;
            Drives.SelectedIndex = 0;
        }


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

        public void SearchFiles()
        {
            _model.SearchFiles(SearchPath.Text, FileNameMask.Text);
        }

        #endregion


        #region INotifyPropertyChanged Members
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

        private void Model_Changed(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_model.IsRunning))
            {

                StartSearch.Content = _model.IsRunning ? _contentStopSearch: _contentStartSearch;
                Drives.IsEnabled = !_model.IsRunning;
                FileNameMask.IsEnabled = SearchPath.IsEnabled = FileNameMask.IsEnabled = !_model.IsRunning;
                if (_model.IsRunning)
                {
                }
                else
                {
                }
            }
        }

        #endregion
    }
}
