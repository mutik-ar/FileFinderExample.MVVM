using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using FileFinderExample.Interfaces;

namespace FileFinderExample.ViewModel
{
    public class ViewModel: INotifyPropertyChanged, IViewModel
    {
        #region Fields

        private IModel _model;

        private vmComboBox<DriveInfoItem> _drives;
        private vmTextBox _searchPath;
 
        #endregion

        #region Properties

        public vmComboBox<DriveInfoItem> Drives
        {
            get
            {
                return _drives;
            }

        }

        public vmTextBox SearchPath 
        { 
            get
            {
                return _searchPath;
            }
        }

        #endregion

        #region Constructor

        public ViewModel(IModel model)
        {
            _model = model;
            _drives = new vmComboBox<DriveInfoItem>(_model.DrivesList);
            _drives.PropertyChanged += Drives_Changed;
            _searchPath = new();
        }
        //public ViewModel()
        //{
        //    _model = new Model.Model();
        //}

        #endregion

        #region Public Methods

        #endregion


        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChangedEventHandler? handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void Drives_Changed(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Drives.List))
            {
            }
            if (e.PropertyName == nameof(Drives.SelectedItem))
            {
                var selectedItem = Drives.SelectedItem;
                if (selectedItem is DriveInfoItem driveInfoItem)
                {
                    string selectedPath = driveInfoItem.DriveName;
                    SearchPath.Text = selectedPath;
                    //FileSearchInfoHolder.SearchDirectory = selectedPath;
                    //UpdateSearchPathReadonlyTextBox(selectedPath);
                }


            }
            if (e.PropertyName == nameof(Drives.SelectedIndex))
            {

            }
        }


        #endregion
    }
}
