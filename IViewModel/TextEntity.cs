
using Shared;


namespace Interfaces.ViewModel
{
    public class TextEntity : VisualEntity
    {

        #region Fields

        private string _text;

        #endregion

        #region Constructors

        public TextEntity()
        {
            _text = string.Empty;
        }

        public TextEntity(string text)
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
