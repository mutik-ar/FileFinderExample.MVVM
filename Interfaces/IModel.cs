using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileFinderExample.Interfaces
{
    public interface IModel
    {
        #region Fields
        ObservableCollection<DriveInfoItem> DrivesList { get; }

        #endregion

        #region Methods
        void SetSearchPath(string searchPath);

        #endregion
    }
}
