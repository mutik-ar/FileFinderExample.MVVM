using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ListAdv<T> : List<T>
    {
        private object _lock = new();

        public ListAdv(ListAdv<T> list = null)
        {
            if (list != null)
            {
                this.AddRange(list);
            }
        }

        public void Clear()
        {
            lock (_lock)
            {
                base.Clear();
            }
        }

    }
}
