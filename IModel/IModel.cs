using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        FileSearchInfo FileSearchInfoHolder { get; } 
        #endregion

        #region Methods
        void SetSearchPath(string searchPath);

        #endregion
    }
}
