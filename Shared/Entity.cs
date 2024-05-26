using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Shared
{
    public class Entity : INotifyPropertyChanged
    {
        #region Events 

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChangedEventHandler? handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        #endregion

    }
}
