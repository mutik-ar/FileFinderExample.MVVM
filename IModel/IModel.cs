using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Model
{
    public interface IModel
    {
        #region Fields

        #endregion

        #region Properties
        ObservableCollection<DriveInfoItem> DrivesList { get; }

        public bool IsRunning { get; set; }
        public long FilesTotalCount { get; set; }

        #endregion

        #region Methods

        void FilesAction(string searchDirectory, string fileNameMask);

        #endregion

        #region Event

        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

    }
}
