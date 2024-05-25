


namespace RecourceCloud.Tests
{

    public class Tests
    {
        private DepartmentCloud departmentCloud;

        [SetUp]
        public void Setup()
        {
            departmentCloud = new DepartmentCloud();
        }

        [Test]
        public void LogTask_AllArgumentsProvided_TaskLoggedSuccessfully()
        {
            string[] args = { "1", "Label", "ResourceName" };
            var result = departmentCloud.LogTask(args);
            Assert.AreEqual("Task logged successfully.", result);
            Assert.AreEqual(1, departmentCloud.Tasks.Count);
            Assert.AreEqual("ResourceName", departmentCloud.Tasks.First().ResourceName);
        }

        [Test]
        public void LogTask_MissingArguments_ArgumentExceptionThrown()
        {
            var departmentCloud = new DepartmentCloud();
            string[] args = { "1", "Label" };
            Assert.Throws<ArgumentException>(() => departmentCloud.LogTask(args));
        }

        [Test]
        public void LogTask_NullArguments_ArgumentExceptionThrown()
        {
            var departmentCloud = new DepartmentCloud();
            string[] args = { "1", null, "ResourceName" };
            Assert.Throws<ArgumentException>(() => departmentCloud.LogTask(args));
        }

        [Test]
        public void LogTask_TaskAlreadyExists_ReturnsTaskAlreadyLoggedMessage()
        {
            var departmentCloud = new DepartmentCloud();
            departmentCloud.LogTask(new string[] { "1", "Label", "ResourceName" });
            var result = departmentCloud.LogTask(new string[] { "2", "AnotherLabel", "ResourceName" });
            Assert.AreEqual("ResourceName is already logged.", result);
            Assert.AreEqual(1, departmentCloud.Tasks.Count);
        }

        [Test]
        public void CreateResource_TasksExist_ResourceCreatedSuccessfully()
        {
            var departmentCloud = new DepartmentCloud();
            departmentCloud.LogTask(new string[] { "1", "Label", "ResourceName" });
            var result = departmentCloud.CreateResource();
            Assert.IsTrue(result);
            Assert.AreEqual(0, departmentCloud.Tasks.Count);
            Assert.AreEqual(1, departmentCloud.Resources.Count);
            Assert.AreEqual("ResourceName", departmentCloud.Resources.First().Name);
        }

        [Test]
        public void CreateResource_NoTasks_ReturnsFalse()
        {
            var departmentCloud = new DepartmentCloud();
            var result = departmentCloud.CreateResource();
            Assert.IsFalse(result);
            Assert.AreEqual(0, departmentCloud.Resources.Count);
        }

        [Test]
        public void TestResource_ResourceExists_ResourceIsTested()
        {
            var departmentCloud = new DepartmentCloud();
            departmentCloud.LogTask(new string[] { "1", "Label", "ResourceName" });
            departmentCloud.CreateResource();
            var result = departmentCloud.TestResource("ResourceName");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsTested);
        }

        [Test]
        public void TestResource_ResourceDoesNotExist_ReturnsNull()
        {
            var departmentCloud = new DepartmentCloud();
            var result = departmentCloud.TestResource("NonExistingResource");
            Assert.IsNull(result);
        }
    }
}
