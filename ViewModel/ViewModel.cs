﻿using System.ComponentModel;
using Interfaces.ViewModel;
using Interfaces.Model;
using System.Timers;
using Shared;




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
        private vmListView<FileProperty> _listViewFoundFiles;

        private System.Timers.Timer _timer;


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

        

        public vmListView<FileProperty> ListViewFoundFiles
        {
            get
            {
                return _listViewFoundFiles ?? (_listViewFoundFiles = new vmListView<FileProperty>());
            }

        }

        #endregion

        #region Constructor

        public ViewModel(IModel model, RefreshMode refreshMode) //
        {
            _model = model;
            Drives.PropertyChanged += Drives_Changed;
            Drives.SelectedIndex = 0;
            switch (refreshMode.mode)
            {
                case Mode.Events:
                    _model.PropertyChanged += OnModelChanged; // обновление UI через события
                    break;
                case Mode.Timer:
                    SetTimer(refreshMode.interval); // запуск таймера  с интервалом 0.5 и обновление UI через список измененных свойств
                    break;
                case Mode.Refresh:
                    // обновление UI через вызов процедуры Refresh 
                    break;
            }
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
        private void OnModelChanged(object? sender, PropertyChangedEventArgs e)
        {
            Model_Changed(e.PropertyName);
        }
        private void Model_Changed(string propertyName)
        {
            if (propertyName == nameof(_model.FilesTotalCount))
            {
                FilesCount.Text = $"Найдено: {_model.FilesTotalCount} файлов.";
            }
            if (propertyName == nameof(_model.FilesFound))
            {
                FilesCount.Text = $"Найдено: {_model.FilesFound.Count} файлов.";
                ListViewFoundFiles.List = new(_model.FilesFound); 
            }
            if (propertyName == nameof(_model.FilesProcessedPercent))
            {
                ProgressBar.Value = _model.FilesProcessedPercent;
            }
            if (propertyName == nameof(_model.State))
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
            if (propertyName == nameof(_model.BlockAction))
            {
                StartSearch.IsEnabled = !_model.BlockAction;
            }
        }


        private void SetTimer(double interval)
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
            Refresh();

        }

        /// <summary>
        /// Обновление свойств ViewModel при запросе
        /// </summary>
        public void Refresh() 
        {
            ListAdv<string> list = new(_model.PropertyChangedList);
            _model.PropertyChangedList.Clear();

            for (int i = 0; i < list.Count(); i++)
            {
                Model_Changed(list[i]);
            }

        }


        #endregion
    }
}
