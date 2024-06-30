using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Shared
{
    public class Entity : INotifyPropertyChanged, IPropertyChangedList
    {
        #region Fields
        private ListAdv<string> _propertyChangedList = new();

        #endregion

        #region Properties
        public ListAdv<string> PropertyChangedList => _propertyChangedList;

        #endregion


        #region Events 

        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        #region Methods

        protected void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChangedEventHandler? handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(prop));
            AddDistinct(prop);
        }


        protected void AddDistinct(string propertyName)
        {
            if (!_propertyChangedList.Contains(propertyName))
            {
                _propertyChangedList.Add(propertyName);
            }
        }

        #endregion

    }
}
