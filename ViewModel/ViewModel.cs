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
        private vmTextBlock _filesCount;
        private vmTextBlock _progressText;
        private vmProgressBar _progressBar;
        private string _contentStopSearch = "Прервать";
        private string _contentStartSearch = "Начать поиск";
        private ActionCommand _filesActionCommand;

        #endregion



        #region Properties

        public vmComboBox<DriveInfoItem> Drives
        {
            get
            {
                return _drives ?? (_drives = new vmComboBox<DriveInfoItem>(_model.DrivesList));
            }

        }

        public vmTextBox SearchPath 
        { 
            get
            {
                return _searchPath ?? (_searchPath = new());
            }
        }

        public vmTextBox FileNameMask
        {
            get
            {
                return _fileNameMask ?? (_fileNameMask = new());
            }
        }

        public vmButton StartSearch
        {
            get
            {
                return _startSearch ?? (_startSearch = new());
            }
        }

        public vmTextBlock FilesCount
        {
            get
            {
                return _filesCount ?? (_filesCount = new());
            }
        }

        public vmTextBlock ProgressText
        {
            get
            {
                return _progressText ?? (_progressText = new());
            }
        }

        public vmProgressBar ProgressBar
        {
            get
            {
                return _progressBar ?? (_progressBar = new());
            }
        }

        public ActionCommand FilesActionCommand
        {
            get
            {
                return _filesActionCommand ?? (_filesActionCommand = new(FilesAction));
            }
        }


        #endregion

        #region Constructor

        public ViewModel(IModel model)
        {
            _model = model;
            Drives.PropertyChanged += Drives_Changed;
            Drives.SelectedIndex = 0;
            _model.PropertyChanged += Model_Changed;
            StartSearch.Content = _contentStartSearch;
            FilesCount.IsVisible = ProgressText.IsVisible = ProgressBar.IsVisible = false;
 
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

        private void FilesAction()
        {
            _model.FilesAction(SearchPath.Text, FileNameMask.Text);
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
            if (e.PropertyName == nameof(_model.FilesTotalCount))
            {
                FilesCount.Text = $"Найдено: {_model.FilesTotalCount} файлов. Статус: {_model.State}";
            }
            if (e.PropertyName == nameof(_model.FilesFound))
            {
                FilesCount.Text = $"Найдено: {_model.FilesFound} файлов. Статус: {_model.State}";
            }
            if (e.PropertyName == nameof(_model.State))
            {
                switch (_model.State)
                {
                    case States.Start:
                        ProgressText.Text = "";
                        break;
                    case States.EstimateProccess:
                        ProgressText.Text = "Оценка количества файлов в каталоге в процессе...";
                        ProgressText.IsVisible = FilesCount.IsVisible = ProgressBar.IsVisible = true;
                        StartSearch.Content = _contentStopSearch;
                        Drives.IsEnabled = FileNameMask.IsEnabled = SearchPath.IsEnabled = FileNameMask.IsEnabled = false;
                        ProgressBar.IsIndeterminate = true;
                        break;
                    case States.EstimateComplited:
                        ProgressText.Text = "Оценка количества файлов в каталоге завершена...";
                        break;
                    case States.EstimateCanceled:
                        ProgressText.Text = "Оценка количества файлов в каталоге прервана...";
                        StartSearch.Content = _contentStartSearch;
                        Drives.IsEnabled = FileNameMask.IsEnabled = SearchPath.IsEnabled = FileNameMask.IsEnabled = true;
                        ProgressBar.IsIndeterminate = false;
                        break;
                    case States.FilesSearchProccess:
                        ProgressText.Text = "Подсчёт количества файлов в системе, согласно маске в процессе ...";
                        StartSearch.Content = _contentStopSearch;
                        Drives.IsEnabled = FileNameMask.IsEnabled = SearchPath.IsEnabled = FileNameMask.IsEnabled = false;
                        ProgressBar.IsIndeterminate = false;
                        break;
                    case States.FilesSearchComplited:
                        ProgressText.Text = "Подсчёт количества файлов в системе, согласно маске завершен ...";
                        break;
                    case States.FilesSearchCanceled:
                        ProgressText.Text = "Подсчёт количества файлов в системе, согласно маске прерван ...";
                        StartSearch.Content = _contentStartSearch;
                        Drives.IsEnabled = FileNameMask.IsEnabled = SearchPath.IsEnabled = FileNameMask.IsEnabled = true;
                        ProgressBar.IsIndeterminate = false;
                        break;
                    case States. Finish:
                        ProgressText.Text = "Процесс успешно завершен";
                        StartSearch.Content = _contentStartSearch;
                        Drives.IsEnabled = FileNameMask.IsEnabled = SearchPath.IsEnabled = FileNameMask.IsEnabled = true;
                        ProgressBar.IsIndeterminate = false;
                        break;
                }

            }
            if (e.PropertyName == nameof(_model.BlockAction))
            {
                StartSearch.IsEnabled = !_model.BlockAction;
            }
        }

        #endregion
    }
}
