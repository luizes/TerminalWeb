namespace TerminalWeb.Domain.ViewModels
{
    public class DiskDriveViewModel
    {
        public DiskDriveViewModel() { }

        public DiskDriveViewModel(string name, long totalSize)
        {
            Name = name;
            TotalSize = totalSize;
        }

        public string Name { get; set; }
        public long TotalSize { get; set; }
    }
}
