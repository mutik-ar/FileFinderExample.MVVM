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
        RefreshMode()
        {
            this.mode = Mode.Refresh;
            this.interval = 100;
        }

        public Mode mode { get; }
        public int interval { get; }
    }
}
