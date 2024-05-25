using TheContentDepartment.Utilities.Messages;

namespace TheContentDepartment.Models
{
    public class ContentMember : TeamMember
    {
        public ContentMember(string name, string path)
            : base(name, path)
        {
            string[] validPaths = { "CSharp", "JavaScript", "Python", "Java" };
            if (!Array.Exists(validPaths, p => p == path))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.PathIncorrect, path));
            }
        }

        public override string ToString()
        => $"{Name} - {Path} path. Currently working on {InProgress.Count} tasks.";
    }
}
