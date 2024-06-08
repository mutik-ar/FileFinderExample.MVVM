using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Model
{
    public enum States
    {
        Start,
        EstimateProccess,
        EstimateComplited,
        EstimateCanceled,
        FilesSearchProccess,
        FilesSearchComplited,
        FilesSearchCanceled,
        Finish
    }
}
