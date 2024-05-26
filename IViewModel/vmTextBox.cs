﻿
using Shared;


namespace Interfaces.ViewModel
{
    public class vmTextBox : VisualEntity
    {

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
