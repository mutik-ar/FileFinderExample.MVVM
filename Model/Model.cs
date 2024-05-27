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

        /// <summary>
        /// Текущий режим работы BackgroundWorker элементов и рекурсивного обхода директорий: 
        /// Estimate - происходит оценка времени на поиск
        /// Search - происходит непосредственно поиск файлов по заданной маске
        /// </summary>
        enum ActionMode
        {
            Estimate,
            Search
        }

        #endregion


        #region Properties

        public ObservableCollection<DriveInfoItem> DrivesList
        {
            get
            {
                return _drivesList;
            }
        }

        public bool IsRunning 
        {
            get
            {
                return _fileSearchInfoHolder.IsRunning;
            }
            set
            {
                _fileSearchInfoHolder.IsRunning = value;
                OnPropertyChanged();
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

        #endregion

        #region Constructors
        public Model()
        {
            LoadAvailableDrivesInfo();
        }
        #endregion


        #region public Methods
        public void SearchFiles(string searchDirectory, string fileNameMask)
        {
            if (!IsRunning)
            {
                IsRunning = true;
                _fileSearchInfoHolder.SearchDirectory = searchDirectory;
                _fileSearchInfoHolder.FileNameMask = fileNameMask;
                FilesTotalCount = 0;
                _fileSearchInfoHolder.FilesFound = 0;
                _fileSearchInfoHolder.FoundFiles.Clear();
                CalculateFilesCountRecursively(searchDirectory, ActionMode.Estimate, _fileSearchInfoHolder);
            }
            else
            {
                IsRunning = false;
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
        private void CalculateFilesCountRecursively(string parentDirectory, ActionMode _actionMode, FileSearchInfo fileInfoHolder)
        {
            try
            {
                IEnumerable<string> subdirectories = Directory.EnumerateDirectories(parentDirectory, "*", SearchOption.TopDirectoryOnly);
                IEnumerable<string> files = Directory.EnumerateFiles(parentDirectory);

                if (_actionMode == ActionMode.Estimate)
                {
                    // если было запрошено прерывание операции оценки времени поиска - выходим из рекурсии
                    //if (BackgroundWorkerEstimateSearchTime.CancellationPending)
                    //{
                    //    return;
                    //}

                    FilesTotalCount += files.LongCount();
                    //BackgroundWorkerEstimateSearchTime.ReportProgress(10);
                }
                else if (_actionMode == ActionMode.Search)
                {

                    // если было запрошено прерывание операции поиска - выходим из рекурсии
                    //if (BackgroundWorkerSearchFiles.CancellationPending)
                    //{
                    //    return;
                    //}
                    List<string> foundFiles = new();

                    foreach (string file in files)
                    {
                        if (file.Contains(fileInfoHolder.FileNameMask))
                        {
                            fileInfoHolder.FoundFiles.Add(file);
                            foundFiles.Add(file);
                            fileInfoHolder.FilesFound++;
                        }
                    }


                    fileInfoHolder.FilesProcessedCount += files.LongCount();
                    int progress = (int)(fileInfoHolder.FilesProcessedCount * 100 / fileInfoHolder.FilesTotalCount);
                    //BackgroundWorkerSearchFiles.ReportProgress(progress, foundFiles);
                    fileInfoHolder.FoundFiles.Clear();
                }

                if (subdirectories.LongCount() > 0)
                {
                    foreach (string subdirectory in subdirectories)
                    {
                        CalculateFilesCountRecursively(subdirectory, _actionMode, fileInfoHolder);
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
