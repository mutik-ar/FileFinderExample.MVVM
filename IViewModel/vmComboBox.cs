using System.Collections.ObjectModel;

namespace Interfaces.ViewModel
{
    public class vmComboBox<T> : ListEntity<T>
    {


        #region Fields


        #endregion

        #region Constructors

        public vmComboBox()
        {
        }

        public vmComboBox(ObservableCollection<T> list) : base(list)
        {
        }

        public vmComboBox(ObservableCollection<T> list, string text) : base(list, text)
        {
        }


        #endregion

        #region Properties

        #endregion
    }
}
