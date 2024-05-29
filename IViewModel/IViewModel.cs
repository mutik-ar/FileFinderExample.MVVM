using Interfaces.Model;

namespace Interfaces.ViewModel
{
    public interface IViewModel
    {
        #region Properties

        vmComboBox<DriveInfoItem> Drives { get; }
        vmTextBox SearchPath { get; }
        vmTextBox FileNameMask { get; }
        vmTextBlock FilesCount { get; }
        vmTextBlock ProgressText { get; }
        vmProgressBar ProgressBar { get; }

        #endregion

        #region Methods

        void UpdateSearchPathReadonlyTextBox(string searchPath);
        void FilesAction();

        #endregion

    }
}
