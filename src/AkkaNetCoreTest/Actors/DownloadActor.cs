using System;
using System.Net.Http;
using Akka.Actor;
using AkkaNetCoreApp.Models;

namespace AkkaNetCoreApp.Actors
{
    public class DownloadActor : ReceiveActor
    {
        public DownloadActor()
        {
            Receive<DownloadCommand>(download =>
            {
                var client = new HttpClient();
                client.GetAsync(download.DownloadUrl).PipeTo(Self, Self, message => new DownloadHttpResponse(message));
            });

            Receive<DownloadHttpResponse>(response =>
            {
                response.HttpResponseMessage.Content.ReadAsStringAsync().PipeTo(Self, Self, message => new DownloadHttpContent(message));
            });

            Receive<DownloadHttpContent>(content =>
            {
                Console.WriteLine($"Html was sucesfully downloaded; Content-Lenght={content.Content.Length}");
            });
        }
    }
}
