namespace AkkaNetCoreApp.Models
{
    public class ChangeIteration
    {
        public ChangeIteration(string newIterationName)
        {
            NewIterationName = newIterationName;
        }

        public string NewIterationName { get; }
    }
}
