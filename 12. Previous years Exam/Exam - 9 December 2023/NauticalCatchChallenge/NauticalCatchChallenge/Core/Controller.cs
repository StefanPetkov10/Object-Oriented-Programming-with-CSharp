using NauticalCatchChallenge.Core.Contracts;
using NauticalCatchChallenge.Models;
using NauticalCatchChallenge.Models.Contracts;
using NauticalCatchChallenge.Repositories;
using NauticalCatchChallenge.Repositories.Contracts;
using NauticalCatchChallenge.Utilities.Messages;
using System.Text;

namespace NauticalCatchChallenge.Core
{
    public class Controller : IController
    {
        private IRepository<IDiver> divers;
        private IRepository<IFish> fish;

        public Controller()
        {
            divers = new DiverRepository();
            fish = new FishRepository();
        }
        public string DiveIntoCompetition(string diverType, string diverName)
        {
            string result = string.Empty;

            if (diverType != nameof(FreeDiver) && diverType != nameof(ScubaDiver))
            {
                result = string.Format(OutputMessages.DiverTypeNotPresented, diverType);
            }
            else if (divers.GetModel(diverName) != null)
            {
                result = string.Format(OutputMessages.DiverNameDuplication, diverName, nameof(DiverRepository));
            }
            else
            {
                IDiver diver;

                if (diverType == nameof(FreeDiver))
                {
                    diver = new FreeDiver(diverName);
                }
                else
                {
                    diver = new ScubaDiver(diverName);
                }

                divers.AddModel(diver);
                result = string.Format(OutputMessages.DiverRegistered, diverName, nameof(DiverRepository));
            }
            return result.Trim();
        }
        public string SwimIntoCompetition(string fishType, string fishName, double points)
        {
            string result = string.Empty;

            if (fishType != nameof(ReefFish) && fishType != nameof(DeepSeaFish) && fishType != nameof(PredatoryFish))
            {
                result = string.Format(OutputMessages.FishTypeNotPresented, fishType);
            }

            else if (fish.GetModel(fishName) != null)
            {
                result = string.Format(OutputMessages.FishNameDuplication, fishName, nameof(FishRepository));
            }
            else
            {
                IFish currFish;

                if (fishType == nameof(ReefFish))
                {
                    currFish = new ReefFish(fishName, points);
                }
                else if (fishType == nameof(DeepSeaFish))
                {
                    currFish = new DeepSeaFish(fishName, points);
                }
                else
                {
                    currFish = new PredatoryFish(fishName, points);
                }

                fish.AddModel(currFish);
                result = string.Format(OutputMessages.FishCreated, fishName);
            }
            return result.TrimEnd();
        }

        public string ChaseFish(string diverName, string fishName, bool isLucky)
        {
            if (divers.GetModel(diverName) == null)
            {
                return string.Format(OutputMessages.DiverNotFound, diverName);
            }

            if (fish.GetModel(fishName) == null)
            {
                return string.Format(OutputMessages.FishNotAllowed, fishName);
            }

            IDiver diver = divers.GetModel(diverName);

            if (diver.HasHealthIssues == true)
            {
                return string.Format(OutputMessages.DiverHealthCheck, diverName);
            }

            IFish currFish = fish.GetModel(fishName);

            if (diver.OxygenLevel < currFish.TimeToCatch)
            {
                diver.Miss(currFish.TimeToCatch);

                if (diver.OxygenLevel == 0)
                {
                    diver.UpdateHealthStatus();
                }

                return string.Format(OutputMessages.DiverMisses, diverName, fishName);
            }
            else if (diver.OxygenLevel == currFish.TimeToCatch)
            {
                if (isLucky == true)
                {
                    diver.Hit(currFish);

                    if (diver.OxygenLevel == 0)
                    {
                        diver.UpdateHealthStatus();
                    }

                    return string.Format(OutputMessages.DiverHitsFish, diverName, currFish.Points, fishName);
                }
                else
                {
                    diver.Miss(currFish.TimeToCatch);

                    if (diver.OxygenLevel == 0)
                    {
                        diver.UpdateHealthStatus();
                    }

                    return string.Format(OutputMessages.DiverMisses, diverName, fishName);
                }
            }
            else
            {
                diver.Hit(currFish);
                if (diver.OxygenLevel == 0)
                {
                    diver.UpdateHealthStatus();
                }
                return string.Format(OutputMessages.DiverHitsFish, diverName, currFish.Points, fishName);
            }
        }
        public string HealthRecovery()
        {
            int counter = 0;

            foreach (var d in divers.Models.Where(x => x.HasHealthIssues == true))
            {
                counter++;
                d.UpdateHealthStatus();
                d.RenewOxy();
            }

            return string.Format(OutputMessages.DiversRecovered, counter);
        }

        public string DiverCatchReport(string diverName)
        {
            IDiver diver = divers.GetModel(diverName);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(diver.ToString());
            sb.AppendLine("Catch Report:");

            foreach (var fishName in diver.Catch)
            {
                IFish currFish = fish.GetModel(fishName);
                sb.AppendLine(currFish.ToString());
            }

            return sb.ToString().Trim();
        }
        public string CompetitionStatistics()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("**Nautical-Catch-Challenge**");

            foreach (var d in divers.Models.Where(x => x.HasHealthIssues == false).OrderByDescending(x => x.CompetitionPoints).ThenByDescending(x => x.Catch.Count).ThenBy(x => x.Name))
            {
                sb.AppendLine(d.ToString());
            }

            return sb.ToString().TrimEnd();
        }


    }
}
