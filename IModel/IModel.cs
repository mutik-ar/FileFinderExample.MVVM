using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Shared;

namespace Interfaces.Model
{
    public interface IModel
    {
        #region Fields

        #endregion

        #region Properties
        ObservableCollection<DriveInfoItem> DrivesList { get; }
        long FilesTotalCount { get; set; }
        ObservableCollection<FileProperty> FilesFound { get; }
        States  State {get; set;}
        bool BlockAction { get; set; }
        public int FilesProcessedPercent { get; set; }
        public ListAdv<string> PropertyChangedList { get; }

        #endregion

        #region Methods

        void FilesAction(string searchDirectory, string fileNameMask);


        #endregion

        #region Event

        event PropertyChangedEventHandler? PropertyChanged;

        #endregion

    }
}
