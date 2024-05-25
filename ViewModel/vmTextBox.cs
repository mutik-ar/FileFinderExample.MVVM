using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FileFinderExample.ViewModel
{
    public class vmTextBox : INotifyPropertyChanged
    {
        #region Events 

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChangedEventHandler? handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        #endregion


        #region Fields

        private string _text;

        #endregion

        #region Constructors

        public vmTextBox()
        {
            _text = string.Empty;
        }

        public vmTextBox(string text)
        {
            _text = text;
        }


        #endregion

        #region Properties

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        #endregion

    }
}
