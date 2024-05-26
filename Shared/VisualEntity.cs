using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Shared
{
    public class VisualEntity : Entity
    {
        #region Fields
        private bool  _isEnabled = true;
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

        #endregion

    }
}
