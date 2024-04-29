namespace App.Model
{
    public sealed class Settings
    {
        public string RootDirectory { get; set; }
        public string FileSource { get; set; }
        public string FileTarget { get; set; }
        public string SourceFilePath { get; set; }
        public string SubDirectoryToSeachFileTarget { get; set; }
        public bool IncludeTargetDifferentKeys { get; set; }
    }
}
