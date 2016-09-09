using System;
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
            if (message is SaveSnapshotSuccess)
            {
                Console.WriteLine($"Snapshot successfully saved");
            }
            else
            {
                Console.WriteLine($"Saving of incoming message of {message.GetType()}...");
                Persist(message, evt =>
                {
                    Console.WriteLine($"Message of {evt.GetType()} has been persisted");
                    UpdateState(evt);
                    if (evt is AddNewMemberToTeam && State.Members.Count % 5 == 0)
                    {
                        Console.WriteLine($"Saving a snapshot of {typeof(DevelopmentTeamState)}...");
                        SaveSnapshot(State);
                    }
                });
            }
        }

        protected override void OnRecover(object message)
        {
            if (message is RecoveryCompleted)
            {
                Console.WriteLine($"Recovery of the state has been completed");
            }
            else if (message is SnapshotOffer)
            {
                var snapshotOffer = (SnapshotOffer)message;
                State = snapshotOffer.Snapshot as DevelopmentTeamState;
            }
            else
            {
                Console.WriteLine($"Message of {message.GetType()} has been recovered");
                UpdateState(message);
            }
        }

        public override string PersistenceId { get; }

        private DevelopmentTeamState State { get; set; }

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
