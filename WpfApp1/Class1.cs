using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
    internal class Class1 : INotifyPropertyChanged
    {

        private Visibility visibility;
        public Visibility Visibility1
        {
            get
            {
                return visibility;
            }
            set
            {
                visibility = value;

                OnPropertyChanged1();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged1([CallerMemberName] string prop = "")
        {
            PropertyChangedEventHandler? handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
