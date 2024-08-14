using Interfaces.Model;
using Shared;

namespace Interfaces.ViewModel
{
    public interface IViewModel
    {
        #region Properties

        RefreshModes RefreshMode { get; }
        int RefreshInterval { get; }
        vmComboBox<DriveInfoItem> Drives { get; }
        vmButton StartSearch { get; }
        vmTextBox SearchPath { get; }
        vmTextBox FileNameMask { get; }
        vmTextBlock FilesCount { get; }
        vmTextBlock ProgressText { get; }
        vmTextBlock StatusText { get; }
        vmProgressBar ProgressBar { get; }
        ActionCommand FilesActionCommand { get; }
        ActionCommand RefreshActionCommand { get; }
        vmListView<FileProperty> ListViewFoundFiles { get; }

        #endregion

        #region Methods

        void UpdateSearchPathReadonlyTextBox(string searchPath);


        #endregion

    }
}
