using Interfaces.Model;

namespace Interfaces.ViewModel
{
    public interface IViewModel
    {
        #region Properties

        vmComboBox<DriveInfoItem> Drives { get; }
        vmTextBox SearchPath { get; }
        vmTextBox FilePart { get; }

        #endregion

        #region Methods

        void UpdateSearchPathReadonlyTextBox(string searchPath);

        #endregion

    }
}
