using Interfaces.Model;

namespace Interfaces.ViewModel
{
    public interface IViewModel
    {
        vmComboBox<DriveInfoItem> Drives { get; }
        vmTextBox SearchPath { get; }
        vmTextBox FilePart { get; }

    }
}
