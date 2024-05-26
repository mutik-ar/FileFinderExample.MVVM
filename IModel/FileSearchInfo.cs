
namespace Interfaces.Model
{
    public class FileSearchInfo
    {
        public long FilesTotalCount { get; set; } = 0;
        public long FilesProcessedCount { get; set; } = 0;
        public string SearchDirectory { get; set; }
        public long FilesFound { get; set; } = 0;
        public string FileNameMask { get; set; } = "";
        public List<string> FoundFiles = new List<string>();
    }
}
