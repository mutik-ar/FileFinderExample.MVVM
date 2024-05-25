﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileFinderExample.Interfaces
{
    public interface IModel
    {
        ObservableCollection<DriveInfoItem> DrivesList { get; }
    }
}
