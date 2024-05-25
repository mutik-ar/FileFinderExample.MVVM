using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FileFinderExample.Interfaces;
using FileFinderExample.ViewModel;

namespace FileFinderExample.Model
{
    public class Model: IModel, INotifyPropertyChanged
    {
        #region Fields
        private ObservableCollection<DriveInfoItem> _drivesList = new();

        #endregion


        #region Properties

        public ObservableCollection<DriveInfoItem> DrivesList 
        {
            get
            {
              return _drivesList;
            }
        }

        #endregion

        #region Constructors
        public Model()
        {
            LoadAvailableDrivesInfo();
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
