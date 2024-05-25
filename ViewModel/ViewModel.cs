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
        private CancellationTokenSource _cancelTokenSource;
        //private int _iterations = 50;
        //private int _progressPercentage = 0;
        //private string _output;
        //private bool _startEnabled = true;
        //private bool _cancelEnabled = false;

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

        //public void StartProcess()
        //{
        //    Output = "";
        //    StartEnabled = false;
        //    CancelEnabled = true;

        //    _cancelTokenSource = new CancellationTokenSource();
        //    if (_model == null)
        //        _model = new ProcessModel(Iterations);
        //    else
        //        _model.Iterations = Iterations;
        //    var task = DoWorkAsync(_model, _cancelTokenSource.Token,
        //        new Progress<ProgressObject>(UpdateProgress));
        //    task.ContinueWith(TaskComplete);
        //}

        //public void CancelProcess()
        //{
        //    _cancelTokenSource.Cancel();
        //}

        #endregion

        #region BackgroundWorker Events

        // Note: This event fires on the background thread.
        //private Task<int> DoWorkAsync(ProcessModel model, CancellationToken cancelToken,
        //    IProgress<ProgressObject> progress)
        //{
        //    var task = Task<int>.Factory.StartNew(() =>
        //    {
        //        int result = 0;
        //        foreach (var val in model)
        //        {
        //            cancelToken.ThrowIfCancellationRequested();
        //            int percentComplete = (int)((float)val / (float)model.Iterations * 100);
        //            string updateMessage =
        //                string.Format("Iteration {0} of {1}", val, model.Iterations);
        //            progress.Report(new ProgressObject() { Percentage = percentComplete, Message = updateMessage });
        //            result = val;
        //        }
        //        return result;
        //    },
        //    cancelToken);
        //    return task;
        //}

        //private void UpdateProgress(ProgressObject prog)
        //{
        //    ProgressPercentage = prog.Percentage;
        //    Output = prog.Message;
        //}

        //private void TaskComplete(Task<int> task)
        //{
        //    if (task.IsFaulted)
        //    {

        //    }
        //    else if (task.IsCanceled)
        //    {
        //        Output = "Canceled";
        //    }
        //    else if (task.IsCompleted)
        //    {
        //        Output = task.Result.ToString();
        //        ProgressPercentage = 0;
        //    }
        //    StartEnabled = true;
        //    CancelEnabled = false;
        //}

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
