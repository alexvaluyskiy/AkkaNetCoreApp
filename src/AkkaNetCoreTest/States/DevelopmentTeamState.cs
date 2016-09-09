using System.Collections.Generic;

namespace AkkaNetCoreApp.States
{
    public class DevelopmentTeamState
    {
        public string IterationName { get; set; }

        public HashSet<string> Members { get; set; } = new HashSet<string>();
    }
}
