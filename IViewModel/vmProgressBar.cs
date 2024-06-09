
namespace Interfaces.ViewModel
{
    public class vmProgressBar : TextEntity
    {

        #region Fields

        //private string _text;
        private bool _isIndeterminate;
        private int _value;

        #endregion

        #region Constructors

        public vmProgressBar()
        {
        }

        public vmProgressBar(string text) : base(text)
        {
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
        public int Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }



        #endregion

    }
}
