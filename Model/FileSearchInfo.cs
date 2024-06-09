using Interfaces.Model;
using System.Collections.ObjectModel;


namespace Model
{
    internal class FileSearchInfo
    {
        public long FilesTotalCount { get; set; } = 0;
        public long FilesProcessedCount { get; set; } = 0;
        public int FilesProcessedPercent { get; set; } = 0;
        public string SearchDirectory { get; set; }
        public string FileNameMask { get; set; } = "";
        public bool BlockAction { get; set; } = false;
        public States State { get; set; } = States.Start;

        public ObservableCollection<FileProperty> FilesFound = new ();
    }
}
