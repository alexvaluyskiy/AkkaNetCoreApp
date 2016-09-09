using Akka;
using Akka.Persistence;
using AkkaNetCoreApp.Models;
using AkkaNetCoreApp.States;

namespace AkkaNetCoreApp.Actors
{
    public class DevelopmentTeamActor : UntypedPersistentActor
    {
        public DevelopmentTeamActor(string developmentTeamName)
        {
            PersistenceId = developmentTeamName;
            State = new DevelopmentTeamState();
        }

        protected override void OnCommand(object message)
        {
            Persist(message, evt =>
            {
                UpdateState(evt);
                if (evt is AddNewMemberToTeam && State.Members.Count % 5 == 0)
                {
                    SaveSnapshot(State);
                }
            });
        }

        protected override void OnRecover(object message)
        {
            UpdateState(message);
        }

        public override string PersistenceId { get; }

        private DevelopmentTeamState State { get; }

        private void UpdateState(object state)
        {
            state.Match()
                .With<AddNewMemberToTeam>(add =>
                {
                    State.Members.Add(add.MemberName);
                })
                .With<ChangeIteration>(iteration =>
                {
                    State.IterationName = iteration.NewIterationName;
                });
        }
    }
}
