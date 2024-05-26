using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Interfaces.ViewModel
{
    public class vmComboBox<T>: INotifyPropertyChanged
    {
        #region Events 

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChangedEventHandler? handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        #endregion


        #region Fields

        private ObservableCollection<T> _list;
        private T _selectedItem ;
        private int _selectedIndex = -1;

        #endregion

        #region Constructors

        public vmComboBox()
        {
            _list = new();
        }

        public vmComboBox(ObservableCollection<T> list)
        {
            _list = list;
        }


        #endregion

        #region Properties
        public ObservableCollection<T> List
        {
            get
            {
                return _list;
            }

        }

        public T SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }
}
