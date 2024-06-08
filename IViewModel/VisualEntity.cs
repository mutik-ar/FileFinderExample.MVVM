using Shared;

namespace Interfaces.ViewModel
{
    public class VisualEntity : Entity
    {
        #region Fields
        private bool  _isEnabled = true;
        private bool? _isVisible = true;
        #endregion


        #region Properties 

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// true -> Visibly
        /// false -> Hidden
        /// null -> Collapsed
        /// </summary>
        public bool? IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                OnPropertyChanged();
            }
        }

        #endregion

    }
}
