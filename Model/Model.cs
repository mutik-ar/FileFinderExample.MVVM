using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Interfaces.Model;


namespace Model
{
    public class Model: IModel, INotifyPropertyChanged
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


        public FileSearchInfo FileSearchInfoHolder
        {
            get
            {
                return _fileSearchInfoHolder;
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
        public void SetSearchPath(string searchPath)
        {
            _fileSearchInfoHolder.SearchDirectory = searchPath;
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

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChangedEventHandler? handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(prop));
        }


    }
}
