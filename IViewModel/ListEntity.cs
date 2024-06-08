﻿using Shared;
using System.Collections.ObjectModel;



namespace Interfaces.ViewModel
{
    public class ListEntity<T> : TextEntity
    {


        #region Fields

        private ObservableCollection<T> _list;
        private T _selectedItem ;
        private int _selectedIndex = -1;

        #endregion

        #region Constructors

        public ListEntity()
        {
            _list = new();
        }

        public ListEntity(ObservableCollection<T> list)
        {
            _list = list;
        }

        public ListEntity(ObservableCollection<T> list, string text):base(text) 
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