using FileFinderExample.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileFinderExample.Interfaces
{
    public interface IViewModel
    {
        vmComboBox<DriveInfoItem> Drives { get; }
        vmTextBox SearchPath { get; }
        vmTextBox FilePart { get; }

    }
}
