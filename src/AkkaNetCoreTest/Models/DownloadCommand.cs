using System;

namespace AkkaNetCoreApp.Models
{
    public class DownloadCommand
    {
        public DownloadCommand(Uri downloadUrl)
        {
            DownloadUrl = downloadUrl;
        }

        public Uri DownloadUrl { get; }
    }
}
