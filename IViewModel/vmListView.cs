using System.Collections.ObjectModel;

namespace Interfaces.ViewModel
{
    public class vmListView<T> : ListEntity<T>
    {


        #region Fields


        #endregion

        #region Constructors

        public vmListView()
        {
        }

        public vmListView(ObservableCollection<T> list) : base(list)
        {
        }


        #endregion

        #region Properties

        #endregion
    }
}
