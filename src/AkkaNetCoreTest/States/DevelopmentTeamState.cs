using System.Collections.Generic;

namespace AkkaNetCoreApp.States
{
    public class DevelopmentTeamState
    {
        public string IterationName { get; set; }

        public List<string> Members { get; set; } = new List<string>();
    }
}
