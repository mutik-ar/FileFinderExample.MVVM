
using Shared;


namespace Interfaces.ViewModel
{
    public class vmProgressBar : VisualEntity
    {

        #region Fields

        //private string _text;
        private bool _isIndeterminate;

        #endregion

        #region Constructors

        public vmProgressBar()
        {
            //_text = string.Empty;
        }



        #endregion

        #region Properties

        public bool IsIndeterminate
        {
            get { return _isIndeterminate; }
            set
            {
                _isIndeterminate = value;
                OnPropertyChanged();
            }
        }



        #endregion

    }
}
