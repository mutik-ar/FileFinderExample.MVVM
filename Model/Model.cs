using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using Interfaces.Model;
using Shared;



namespace Model
{
    public class Model : Entity, IModel 
    {
        #region Fields
        private ObservableCollection<DriveInfoItem> _drivesList = new();
        // Контекстный объект, содержащий необходимые свойства для обмена данными между формой и потоками для объектов BackgroundWorker        
        private FileSearchInfo _fileSearchInfoHolder = new();


        #endregion


        #region Properties

        public ObservableCollection<DriveInfoItem> DrivesList
        {
            get
            {
                return _drivesList;
            }
        }

        public long FilesTotalCount
        {
            get
            {
                return _fileSearchInfoHolder.FilesTotalCount;
            }
            set
            {
                _fileSearchInfoHolder.FilesTotalCount = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<FileProperty> FilesFound
        {
            get
            {
                return _fileSearchInfoHolder.FilesFound;
            }
         }

        public States State 
        {
            get
            {
                return _fileSearchInfoHolder.State;
            }
            set
            {
                _fileSearchInfoHolder.State = value;
                OnPropertyChanged();
            }
        }

        public bool BlockAction 
        {
            get
            {
                return _fileSearchInfoHolder.BlockAction;
            }
            set
            {
                _fileSearchInfoHolder.BlockAction = value;
                OnPropertyChanged();
            }
        }

        public int FilesProcessedPercent
        {
            get
            {
                return _fileSearchInfoHolder.FilesProcessedPercent;
            }
            set
            {
                _fileSearchInfoHolder.FilesProcessedPercent = value;
                OnPropertyChanged();
            }
        }



        #endregion

        #region Constructors
        public Model()
        {
            LoadAvailableDrivesInfo();
            _fileSearchInfoHolder.FilesFound.CollectionChanged += OnCollectionChanged;
        }
        #endregion


        #region public Methods
        public void FilesAction(string searchDirectory, string fileNameMask)
        {
            States state = State;
            switch (state)
            {
                case States.Start:
                case States.EstimateCanceled:
                case States.Finish:
                    State = States.EstimateProccess;
                    break;
                case States.EstimateProccess:
                    BlockAction = true;
                    State = States.EstimateCanceled;
                    break;
                case States.EstimateComplited:
                    break;
                case States.FilesSearchProccess:
                    BlockAction = true;
                    State = States.FilesSearchCanceled;
                    break;
                case States.FilesSearchComplited:
                    break;
                case States.FilesSearchCanceled:
                    if (
                        _fileSearchInfoHolder.SearchDirectory == searchDirectory
                        &&
                        _fileSearchInfoHolder.FileNameMask == fileNameMask
                       )
                    {
                        State = States.FilesSearchProccess;
                    }
                    else
                    {
                        State = States.EstimateProccess;
                    }
                    break;
            }

            if (State == States.EstimateProccess || State == States.FilesSearchProccess)
            {
                Task.Run(() =>
                {
                    if (State == States.EstimateProccess)
                    {
                        _fileSearchInfoHolder.SearchDirectory = searchDirectory;
                        _fileSearchInfoHolder.FileNameMask = fileNameMask;
                        FilesFound.Clear();
                        FilesTotalCount = 0;
                        CalculateFilesCountRecursively(_fileSearchInfoHolder.SearchDirectory, State);
                        if (State == States.EstimateProccess)
                        {
                            State = States.EstimateComplited;
                        }
                    }
                    if (State == States.FilesSearchProccess || State == States.EstimateComplited)
                    {
                        if (State == States.EstimateComplited)
                        {
                            State = States.FilesSearchProccess;
                        }
                        FilesFound.Clear();
                        _fileSearchInfoHolder.FilesProcessedCount = 0;
                        FilesProcessedPercent = 0;
                        CalculateFilesCountRecursively(_fileSearchInfoHolder.SearchDirectory, State);
                        if (State == States.FilesSearchProccess)
                        {
                            State = States.FilesSearchComplited;
                        }
                    }
                    if (State == States.EstimateComplited || State == States.FilesSearchComplited)
                    {
                        State = States.Finish;
                    }
                    BlockAction = false;
                });
            }
        }

        /// <summary>
        /// Выполняет рекурсивный обход директорий, начиная с родительской директории <paramref name="parentDirectory"/>.
        /// Может работать в двух режимах:
        /// 1) подсчёт общего количества вложенных файлов внутри родительской директории <paramref name="parentDirectory"/>,
        /// 2) поиск в родительской директории <paramref name="parentDirectory"/> файла по заданной маске
        /// </summary>
        /// <param name="parentDirectory">родительская директория, с которой необходимо начать рекурсивный обход вложенных директорий и файлов</param>
        /// <param name="workerMode">режим работы объектов BackgroundWorker: Estimate - оценка времени поиска в каталоге, Search - сам поиск</param>
        /// <param name="fileInfoHolder">контекстный объект, содержащий необходимые свойства для обеспечения оценки поиска и самого поиска</param>
        private void CalculateFilesCountRecursively(String parentDirectory, States state)
        {
            try
            {
                if (State == States.EstimateCanceled  || State == States.FilesSearchCanceled)
                {
                    return; 
                }
                IEnumerable<string> subdirectories = Directory.EnumerateDirectories(parentDirectory, "*", SearchOption.TopDirectoryOnly);
                IEnumerable<string> files = Directory.EnumerateFiles(parentDirectory);

                if (state == States.EstimateProccess)
                {
                    // если было запрошено прерывание операции оценки времени поиска - выходим из рекурсии
                    FilesTotalCount += files.LongCount();
                }
                else if (state == States.FilesSearchProccess)
                {
                    foreach (string file in files)
                    {
                        if (State == States.EstimateCanceled || State == States.FilesSearchCanceled)
                        {
                            return;
                        }
                        if (file.Contains(_fileSearchInfoHolder.FileNameMask))
                        {
                            try
                            {
                                FileInfo fileInfo = new FileInfo(file);
                                long fileSizeInBytes = fileInfo.Length;
                                _fileSearchInfoHolder.FilesFound.Add(new FileProperty() { Path = file, Size = fileSizeInBytes.ToString() });
                            }
                            catch (FileNotFoundException fileNotFoundException)
                            {
                                //TODO: обработать исключение при необходимости...
                            }
                        }
                    }

                    _fileSearchInfoHolder.FilesProcessedCount += files.LongCount();
                    if (_fileSearchInfoHolder.FilesTotalCount > 0)
                    {
                        FilesProcessedPercent = (int)(_fileSearchInfoHolder.FilesProcessedCount * 100 / _fileSearchInfoHolder.FilesTotalCount);
                    }
                    else
                    {
                        FilesProcessedPercent = 0;
                    }
                }

                if (subdirectories.LongCount() > 0)
                {
                    foreach (string subdirectory in subdirectories)
                    {
                        CalculateFilesCountRecursively(subdirectory, state);
                        if (State == States.EstimateCanceled || State == States.FilesSearchCanceled)
                        {
                            return;
                        }
                    }
                }
            }
            catch (UnauthorizedAccessException unauthorizedAccessException)
            {
                // TODO: обработать исключение при необходимости...
            }
            catch (DirectoryNotFoundException directoryNotFoundException)
            {
                // TODO: обработать исключение при необходимости...
            }
            catch (Exception otherException)
            {
                // TODO: обработать исключение при необходимости...
            }
        }


        #endregion

        /// <summary>
        /// Загружает в выпадающий список все доступные в системе диски с краткой информацией о них
        /// </summary>
        private void LoadAvailableDrivesInfo()
        {
            DriveInfo[] driveInfos = DriveInfo.GetDrives();
            foreach (var driveInfo in driveInfos)
            {
                _drivesList.Add(new DriveInfoItem(driveInfo));
            }
        }

        /// <summary>
        /// Обработка события изменения коллекции 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("FilesFound");
        }


    }
}
