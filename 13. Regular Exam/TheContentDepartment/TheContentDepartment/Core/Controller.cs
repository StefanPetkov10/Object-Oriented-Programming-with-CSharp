using System.Text;
using TheContentDepartment.Core.Contracts;
using TheContentDepartment.Models;
using TheContentDepartment.Models.Contracts;
using TheContentDepartment.Repositories;
using TheContentDepartment.Repositories.Contracts;
using TheContentDepartment.Utilities.Messages;

namespace TheContentDepartment.Core
{
    public class Controller : IController
    {
        private IRepository<ITeamMember> teamMembers;
        private IRepository<IResource> resources;

        public Controller()
        {
            this.teamMembers = new MemberRepository();
            this.resources = new ResourceRepository();
        }
        public string ApproveResource(string resourceName, bool isApprovedByTeamLead)
        {
            var resource = resources.Models.FirstOrDefault(r => r.Name == resourceName);

            if (resource.IsTested != true)
            {
                return string.Format(OutputMessages.ResourceNotTested, resourceName);
            }

            var teamLead = teamMembers.Models.FirstOrDefault(m => m is TeamLead) as TeamLead;

            if (isApprovedByTeamLead == true)
            {
                resource.Approve();

                teamLead.FinishTask(resourceName);

                return string.Format(OutputMessages.ResourceApproved, teamLead.Name, resourceName);
            }
            else
            {
                resource.Test();

                return string.Format(OutputMessages.ResourceReturned, teamLead.Name, resourceName);
            }
        }

        public string CreateResource(string resourceType, string resourceName, string path)
        {
            if (resourceType != nameof(Exam) &&
              resourceType != nameof(Workshop) &&
              resourceType != nameof(Presentation))
            {
                return string.Format(OutputMessages.ResourceTypeInvalid, resourceType);
            }

            var contentMember = teamMembers.Models.FirstOrDefault(m => m is ContentMember
            && m.Path == path) as ContentMember;

            if (contentMember == null)
            {
                return string.Format(OutputMessages.NoContentMemberAvailable, resourceName);
            }
            if (contentMember.InProgress.Contains(resourceName))
            {
                return string.Format(OutputMessages.ResourceExists, resourceName);
            }

            IResource resource;

            if (resourceType == nameof(Exam))
            {
                resource = new Exam(resourceName, contentMember.Name);
            }
            else if (resourceType == nameof(Workshop))
            {
                resource = new Workshop(resourceName, contentMember.Name);
            }
            else
            {
                resource = new Presentation(resourceName, contentMember.Name);
            }

            resources.Add(resource);
            return $"{contentMember.Name} created {resourceType} - {resourceName}.";

        }

        public string DepartmentReport()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Finished Tasks:");

            foreach (var resource in resources.Models)
            {
                if (resource.IsApproved == true)
                {
                    sb.AppendLine($"--{resource.ToString()}");
                }
            }

            sb.AppendLine("Team Report:");

            foreach (var member in teamMembers.Models)
            {
                if (member is TeamLead)
                {
                    sb.AppendLine($"--{member.ToString()}");
                }
                else
                {
                    sb.AppendLine(member.ToString());
                }
            }


            return sb.ToString().TrimEnd();
        }

        public string JoinTeam(string memberType, string memberName, string path)
        {
            if (memberType != nameof(TeamLead) &&
              memberType != nameof(ContentMember))
            {
                return string.Format(OutputMessages.MemberTypeInvalid, memberType);
            }

            if (teamMembers.Models.Any(p => p.Path == path))
            {
                return string.Format(OutputMessages.PositionOccupied);
            }
            if (teamMembers.Models.FirstOrDefault(m => m.Name == memberName) != null)
            {
                return string.Format(OutputMessages.MemberExists, memberName);
            }

            ITeamMember member;

            if (memberType == nameof(TeamLead))
            {
                member = new TeamLead(memberName, path);
            }
            else
            {
                member = new ContentMember(memberName, path);
            }

            teamMembers.Add(member);
            return string.Format(OutputMessages.MemberJoinedSuccessfully, memberName);
        }

        public string LogTesting(string memberName)
        {

            if (!teamMembers.Models.Any(n => n.Name == memberName))
            {
                return $"Provide the correct member name.";
            }

            IResource resourceToTest = resources.Models
           .Where(r => r.Creator == memberName && r.IsTested == false)
           .OrderBy(r => r.Priority)
           .FirstOrDefault();

            if (resourceToTest == null)
            {
                return string.Format(OutputMessages.NoResourcesForMember, memberName);
            }

            var teamLead = teamMembers.Models.FirstOrDefault(m => m is TeamLead);

            teamMembers.Models.FirstOrDefault(m => m.Name == memberName).FinishTask(resourceToTest.Name);

            teamLead.WorkOnTask(resourceToTest.Name);

            resourceToTest.Test();

            return string.Format(OutputMessages.ResourceTested, resourceToTest.Name);
        }
    }
}
