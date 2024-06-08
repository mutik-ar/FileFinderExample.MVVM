using System.Collections.ObjectModel;
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
        CancellationTokenSource tokenSource;


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

        public long FilesFound
        {
            get
            {
                return _fileSearchInfoHolder.FilesFound;
            }
            set
            {
                _fileSearchInfoHolder.FilesFound = value;
                OnPropertyChanged();
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


        #endregion

        #region Constructors
        public Model()
        {
            LoadAvailableDrivesInfo();
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
                    _fileSearchInfoHolder.SearchDirectory = searchDirectory;
                    _fileSearchInfoHolder.FileNameMask = fileNameMask;
                    FilesTotalCount = 0;
                    FilesFound = 0;
                    _fileSearchInfoHolder.FoundFiles.Clear();
                    State = States.EstimateProccess;
                    break;
                case States.EstimateProccess:
                    BlockAction = true;
                    tokenSource?.Cancel();
                    break;
                case States.EstimateComplited:
                    break;
                case States.FilesSearchProccess:
                    BlockAction = true;
                    tokenSource?.Cancel();
                    break;
                case States.FilesSearchComplited:
                    break;
                case States.FilesSearchCanceled:
                    FilesFound = 0;
                    _fileSearchInfoHolder.FoundFiles.Clear();
                    State = States.FilesSearchProccess;
                    break;
            }

            if (State == States.EstimateProccess || State == States.FilesSearchProccess)
            {
                tokenSource = new();
                Task.Run(() =>
                {
                    if (State == States.EstimateProccess)
                    {
                        CalculateFilesCountRecursively(_fileSearchInfoHolder.SearchDirectory, State, tokenSource.Token);
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
                        CalculateFilesCountRecursively(_fileSearchInfoHolder.SearchDirectory, State, tokenSource.Token);
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
        private void CalculateFilesCountRecursively(String parentDirectory, States state, CancellationToken token)
        {
            try
            {
                if (token.IsCancellationRequested)
                {
                    if (state == States.EstimateProccess)
                    {
                        State = States.EstimateCanceled;
                    }
                    if (state == States.FilesSearchProccess)
                    {
                        State = States.FilesSearchCanceled;
                    }
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

                    // если было запрошено прерывание операции поиска - выходим из рекурсии
                    List<string> foundFiles = new();

                    foreach (string file in files)
                    {
                        if (token.IsCancellationRequested)
                        {
                            if (state == States.EstimateProccess)
                            {
                                State = States.EstimateCanceled;
                            }
                            if (state == States.FilesSearchProccess)
                            {
                                State = States.FilesSearchCanceled;
                            }
                            return;
                        }
                        if (file.Contains(_fileSearchInfoHolder.FileNameMask))
                        {
                            _fileSearchInfoHolder.FoundFiles.Add(file);
                            foundFiles.Add(file);
                            FilesFound++;
                        }
                    }


                    _fileSearchInfoHolder.FilesProcessedCount += files.LongCount();
                    //int progress = (int)(_fileSearchInfoHolder.FilesProcessedCount * 100 / _fileSearchInfoHolder.FilesTotalCount);
                    _fileSearchInfoHolder.FoundFiles.Clear();
                }

                if (subdirectories.LongCount() > 0)
                {
                    foreach (string subdirectory in subdirectories)
                    {
                        CalculateFilesCountRecursively(subdirectory, state, token);
                        if (token.IsCancellationRequested)
                        {
                            if (state == States.EstimateProccess)
                            {
                                State = States.EstimateCanceled;
                            }
                            if (state == States.FilesSearchProccess)
                            {
                                State = States.FilesSearchCanceled;
                            }
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

    }
}
