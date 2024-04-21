using Handball.Core.Contracts;
using Handball.Models;
using Handball.Models.Contracts;
using Handball.Repositories;
using Handball.Repositories.Contracts;
using Handball.Utilities.Messages;
using System.Linq;
using System.Text;

namespace Handball.Core
{
    public class Controller : IController
    {
        private IRepository<IPlayer> players;
        private IRepository<ITeam> teams;

        public Controller()
        {
            this.players = new PlayerRepository();
            this.teams = new TeamRepository();
        }

        public string LeagueStandings()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"***League Standings***");

            foreach (var team in this.teams.Models.OrderByDescending(t => t.PointsEarned).ThenByDescending(t => t.OverallRating).ThenBy(t => t.Name))
            {
                sb.AppendLine(team.ToString());
            }

            return sb.ToString().TrimEnd();
        }

        public string NewContract(string playerName, string teamName)
        {
            if (!this.players.ExistsModel(playerName))
            {
                return string.Format(OutputMessages.PlayerNotExisting, playerName, nameof(PlayerRepository));
            }
            if (!this.teams.ExistsModel(teamName))
            {
                return string.Format(OutputMessages.TeamNotExisting, teamName, nameof(TeamRepository));
            }

            IPlayer player = players.GetModel(playerName);
            ITeam team = teams.GetModel(teamName);

            if (player.Team != default)
            {
                return string.Format(OutputMessages.PlayerAlreadySignedContract, playerName, player.Team);
            }

            player.JoinTeam(teamName);
            team.SignContract(player);

            return string.Format(OutputMessages.SignContract, playerName, teamName);
        }


        public string NewGame(string firstTeamName, string secondTeamName)
        {
            ITeam firstTeam = this.teams.GetModel(firstTeamName);
            ITeam secondTeam = this.teams.GetModel(secondTeamName);

            if (firstTeam.OverallRating > secondTeam.OverallRating)
            {
                firstTeam.Win();
                secondTeam.Lose();

                return string.Format(OutputMessages.GameHasWinner, firstTeamName, secondTeamName, firstTeamName);
            }

            else if (firstTeam.OverallRating < secondTeam.OverallRating)
            {
                secondTeam.Win();
                firstTeam.Lose();

                return string.Format(OutputMessages.GameHasWinner, secondTeamName, firstTeamName, secondTeamName);
            }

            else
            {
                firstTeam.Draw();
                secondTeam.Draw();

                return string.Format(OutputMessages.GameIsDraw, firstTeamName, secondTeamName);
            }
            //if (firstTeam.OverallRating != secondTeam.OverallRating)
            //{
            //    ITeam winner;
            //    ITeam loser;
            //    if (firstTeam.OverallRating > secondTeam.OverallRating)
            //    {
            //        winner = firstTeam;
            //        loser = secondTeam;
            //    }
            //    else
            //    {
            //        winner = secondTeam;
            //        loser = firstTeam;
            //    }

            //    winner.Win();
            //    loser.Lose();

            //    return string.Format(OutputMessages.GameHasWinner, winner.Name, loser.Name);
            //}
            //else
            //{
            //    firstTeam.Draw();
            //    secondTeam.Draw();

            //    return string.Format(OutputMessages.GameIsDraw, firstTeamName, secondTeamName);
            //}

        }

        public string NewPlayer(string typeName, string name)
        {
            if (typeName != nameof(CenterBack) && typeName != nameof(Goalkeeper) && typeName != nameof(ForwardWing) && typeName != nameof(Goalkeeper))
            {
                return string.Format(OutputMessages.InvalidTypeOfPosition, typeName);
            }
            if (players.ExistsModel(name))
            {
                string position = this.players.GetModel(name).GetType().Name; //or typeName
                return string.Format(OutputMessages.PlayerIsAlreadyAdded, name, nameof(PlayerRepository), position);
            }

            IPlayer player;
            if (typeName == nameof(Goalkeeper))
            {
                player = new Goalkeeper(name);
            }
            else if (typeName == nameof(CenterBack))
            {
                player = new CenterBack(name);
            }
            else
            {
                player = new ForwardWing(name);
            }

            this.players.AddModel(player);
            return string.Format(OutputMessages.PlayerAddedSuccessfully, name, typeName);

        }

        public string NewTeam(string name)
        {
            ITeam team;

            //this.teams.Models.Any(x => x.Name == name)
            if (this.teams.ExistsModel(name))
            {
                return string.Format(OutputMessages.TeamAlreadyExists, name, nameof(TeamRepository));
            }
            team = new Team(name);
            this.teams.AddModel(team);
            // this.teams.AddModel(new Team(name));

            return string.Format(OutputMessages.TeamSuccessfullyAdded, name, nameof(TeamRepository));

        }

        public string PlayerStatistics(string teamName)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"***{teamName}***");
            foreach (var player in this.players.Models.OrderByDescending(p => p.Rating).Where(p => p.Team == teamName))
            {
                if (player.Team == teamName)
                {
                    sb.AppendLine(player.ToString());
                }
            }
            return sb.ToString().TrimEnd();


            //ITeam team = this.teams.GetModel(teamName);

            //foreach (var player in team.Players)
            //{
            //    sb.AppendLine(player.ToString());
            //}

            //return sb.ToString().TrimEnd();
        }
    }
}
