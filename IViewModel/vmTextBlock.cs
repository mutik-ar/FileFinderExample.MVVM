
using Shared;


namespace Interfaces.ViewModel
{
    public class vmTextBlock : VisualEntity
    {

        #region Fields

        private string _text;

        #endregion

        #region Constructors

        public vmTextBlock()
        {
            _text = string.Empty;
        }

        public vmTextBlock(string text)
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
