using System.Collections.Generic;
using System.Text.Json;
using System.Xml.Linq;
using System.Xml.Serialization;
using teko_projektarbeit_loesungsalgorithmen.Objects;

namespace teko_projektarbeit_loesungsalgorithmen.Services
{
    public class DataService
{
        const string ProjectsFileName = "Storage/Projects.json";
        const string UsersFileName = "Storage/Users.json";

        private List<Project> _projects;
        private List<User> _users;

        public List<Project> Projects {
            get => _projects;
            set {
                _projects = value;
                WriteDataToFile<Project>(_projects);
            } 
        }
        public List<User> Users
        {
            get => _users;
            set
            {
                _users = value;
                WriteDataToFile<User>(_users);
            }
        }

        public DataService() 
        {
            Users = ParseDataFile<User>(UsersFileName) ?? CreateDemoUsers();
            Projects = ParseDataFile<Project>(ProjectsFileName) ?? CreateDemoProjects();
        }

        private List<T> ParseDataFile<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }

            using var stream = File.OpenRead(filePath);
            return JsonSerializer.Deserialize<List<T>>(stream);
        }

        private List<Project> CreateDemoProjects()
        {
            return new List<Project>() { new Project("Testproject", Users[0].Id, "Testfirma", "Testprojekt der Testfirma")};
        }
        private List<User> CreateDemoUsers()
        {
            return new List<User> { new User(1, "Test 1", User.ProjectRole.Lead), new User(2, "Test 2", User.ProjectRole.Staff) };
        }

        private void WriteDataToFile<T>(List<T> dataList)
        {
            string filePath = typeof(T) == typeof(Project) ? ProjectsFileName : UsersFileName;

            using var stream = File.OpenWrite(filePath);
            JsonSerializer.Serialize<List<T>>(stream, dataList);
        }

        
    }
}
