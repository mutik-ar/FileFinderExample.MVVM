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
        long FilesTotalCount { get; set; }
        long FilesFound { get; set; }
        States  State {get; set;}

        #endregion

        #region Methods

        void FilesAction(string searchDirectory, string fileNameMask);

        #endregion

        #region Event

        event PropertyChangedEventHandler? PropertyChanged;

        #endregion

    }
}
