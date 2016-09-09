namespace AkkaNetCoreApp.Models
{
    public class DownloadHttpContent
    {
        public DownloadHttpContent(string content)
        {
            Content = content;
        }

        public string Content { get; }
    }
}
