namespace Model
{
    internal class FileSearchInfo
    {
        public long FilesTotalCount { get; set; } = 0;
        public long FilesProcessedCount { get; set; } = 0;
        public string SearchDirectory { get; set; }
        public long FilesFound { get; set; } = 0;
        public string FileNameMask { get; set; } = "";
        public bool IsRunning { get; set; } = false;

        public List<string> FoundFiles = new List<string>();
    }
}
