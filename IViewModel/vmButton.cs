
using Shared;


namespace Interfaces.ViewModel
{
    public class vmButton : VisualEntity
    {

        #region Fields

        private string _content;

        #endregion

        #region Constructors

        public vmButton()
        {
            _content = string.Empty;
        }

        public vmButton(string content)
        {
            _content = content;
        }


        #endregion

        #region Properties

        public string Content
        {
            get { return _content; }
            set
            {
                _content = value;
                OnPropertyChanged();
            }
        }

        #endregion

    }
}
