namespace AkkaNetCoreApp.Models
{
    public class AddNewMemberToTeam
    {
        public AddNewMemberToTeam(string memberName)
        {
            MemberName = memberName;
        }

        public string MemberName { get; }
    }
}
