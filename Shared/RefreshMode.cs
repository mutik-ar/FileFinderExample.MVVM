using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public enum Mode { Events, Timer, Refresh };
    public class RefreshMode
    {
        static public int Interval = 500;
        static public Mode Mode = Mode.Events;
        //public RefreshMode()
        //{
        //    this.mode = Mode.Refresh;
        //    this.interval = 100;
        //}
        public RefreshMode()
        {
            this.mode = Mode;
            this.interval = Interval;
        }

        public Mode mode { get; }
        public int interval { get; }
    }
}
