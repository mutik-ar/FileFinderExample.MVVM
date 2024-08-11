﻿using Interfaces.Model;
using Shared;

namespace Interfaces.ViewModel
{
    public interface IViewModel
    {
        #region Properties

        vmComboBox<DriveInfoItem> Drives { get; }
        vmButton StartSearch { get; }
        vmTextBox SearchPath { get; }
        vmTextBox FileNameMask { get; }
        vmTextBlock FilesCount { get; }
        vmTextBlock ProgressText { get; }
        vmProgressBar ProgressBar { get; }
        ActionCommand FilesActionCommand { get; }
        ActionCommand RefreshActionCommand { get; }
        RefreshMode RefreshMode { get; }

        #endregion

        #region Methods

        void UpdateSearchPathReadonlyTextBox(string searchPath);


        #endregion

    }
}
