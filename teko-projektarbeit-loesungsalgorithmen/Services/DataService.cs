using teko_projektarbeit_loesungsalgorithmen.Objects;

namespace teko_projektarbeit_loesungsalgorithmen.Services
{
    public class DataService
{
        public List<Project> Projects { get; set; }
        public List<User> Users { get; set; }

        public DataService() 
        {
            CreateDemoData();
        }

        public bool SaveData()
        {
            return true;
        }

        public void CreateDemoData()
        {
            Users = new List<User> { new User(1, "Test 1", User.ProjectRole.Lead), new User(2, "Test 2", User.ProjectRole.Staff) };
            Projects = new List<Project>() { new Project("Testproject", Users[0], "Testfirma", "Testprojekt der Testfirma")};
        }
    }
}
