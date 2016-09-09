using System;
using Akka.Actor;
using Akka.Configuration;
using AkkaNetCoreApp.Actors;
using AkkaNetCoreApp.Models;

namespace AkkaNetCoreApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Config config = ConfigurationFactory.ParseString(@"
                akka {
                    loglevel = DEBUG
                    actor {
                        debug {
                            receive = on
                            autoreceive = on
                            lifecycle = on
                            unhandled = on
                        }
                    }

                    persistence {
                        journal {
                            plugin = ""akka.persistence.journal.sqlite""
                            sqlite {
                                class = ""Akka.Persistence.Sqlite.Journal.SqliteJournal, Akka.Persistence.Sqlite""
                                auto-initialize = on
                                connection-string = ""Filename=file:memdb-journal-1.db""
                            }
                        }
                        snapshot-store {
                            plugin = ""akka.persistence.snapshot-store.sqlite""
                            sqlite {
                                class = ""Akka.Persistence.Sqlite.Snapshot.SqliteSnapshotStore, Akka.Persistence.Sqlite""
                                auto-initialize = on
                                connection-string = ""Filename=file:memdb-snapshot-1.db""
                            }
                        }
                    }
                }
            ");

            ActorSystem system = ActorSystem.Create("AkkaNetCore", config);

            IActorRef actor = system.ActorOf(Props.Create<DownloadActor>(), "DownloadActor");
            actor.Tell(new DownloadCommand(new Uri("http://doc.akka.io/docs/akka/snapshot/general/configuration.html")));

            IActorRef teamActor = system.ActorOf(Props.Create(() => new DevelopmentTeamActor("ActiveFinancialsTeam")), "TeamActor");
            teamActor.Tell(new AddNewMemberToTeam("Alex Valuyskiy"));
            teamActor.Tell(new AddNewMemberToTeam("Marc Piechura"));
            teamActor.Tell(new AddNewMemberToTeam("Aaron Stannard"));
            teamActor.Tell(new AddNewMemberToTeam("Bartosz Sypytkowski"));
            teamActor.Tell(new AddNewMemberToTeam("Roger Johansson"));
            teamActor.Tell(new AddNewMemberToTeam("Chris Constantin"));
            teamActor.Tell(new ChangeIteration("Sprint 1"));

            system.WhenTerminated.Wait(1000);
            Console.ReadLine();
        }
    }
}
