using System.Collections.Generic;
using System.Text.Json;
using System.Xml.Linq;
using System.Xml.Serialization;
using teko_projektarbeit_loesungsalgorithmen.Objects;

namespace teko_projektarbeit_loesungsalgorithmen.Services
{
    public class DataService
{
        const string StorageFolder = "Storage/";

        private List<Project> _projects;
        private List<User> _users;
        private List<Information> _informations;

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
        public List<Information> Informations
        {
            get => _informations;
            set
            {
                _informations = value;
                WriteDataToFile<Information>(_informations);
            }
        }

        public DataService() 
        {
            Users = ParseDataFile<User>() ?? CreateDemoUsers();
            Projects = ParseDataFile<Project>() ?? CreateDemoProjects();
            Informations = ParseDataFile<Information>() ?? new List<Information>();
        }

        public User? GetUserById(int userId)
        {
            return _users.Where(i => i.Id == userId).FirstOrDefault();
        }

        private List<T> ParseDataFile<T>()
        {
            string filePath = StorageFolder + typeof(T).Name + "s.json";

            if (!File.Exists(filePath))
            {
                return null;
            }

            using var stream = File.OpenRead(filePath);
            return JsonSerializer.Deserialize<List<T>>(stream);
        }

        private List<Project> CreateDemoProjects()
        {
            return new List<Project>() { new Project(1, "Testproject", Users[0].Id, "Testfirma", "Testprojekt der Testfirma")};
        }
        private List<User> CreateDemoUsers()
        {
            return new List<User> { new User(1, "Test 1", User.ProjectRole.Lead), new User(2, "Test 2", User.ProjectRole.Staff) };
        }

        private void WriteDataToFile<T>(List<T> dataList)
        {
            string filePath = StorageFolder + typeof(T).Name + "s.json";
            string directoryPath = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            using var stream = File.OpenWrite(filePath);
            JsonSerializer.Serialize<List<T>>(stream, dataList);
        }

        // Returns the {limit} newest versions of the project with id {id}
        public List<Project> GetProjectById(int id, int limit = 1)  
        {
            List<Project> returnList = new List<Project>();
            List<Project> queryList = Projects
                .Where(q => q.Id == id)
                .OrderByDescending(q => q.Version)
                .Take(limit)
                .ToList();

            foreach (var query in queryList)
            {
                returnList.Add(query.Copy());
            }

            return returnList;
        }

        public List<Project> GetAllProjects(bool includeVersions = false)
        {
            List<Project> resultList = Projects
                .OrderByDescending(q => q.Id)
                .ThenByDescending(q => q.Version)
                .ToList();

            if (!includeVersions)
            {
                int previousId = 0;
                List<Project> tempList = new List<Project>();

                foreach (Project project in resultList)
                {
                    if (previousId != project.Id)
                    {
                        previousId = project.Id;
                        tempList.Add(project.Copy());
                    }
                }

                resultList = tempList;
            }

            return resultList;
        }

        public List<Information> GetInformationsByProjectId(int id)
        {
            List<Information> resultList = new List<Information>();

            foreach (Information information in Informations) { 
            
                if (information.ProjectId == id)
                {
                    resultList.Add(information);
                }
            }

            return resultList;
        }

        public int GetNewProjectId()
        {
            List<Project> latest = Projects.OrderByDescending(q => q.Id).ToList();

            return (latest.Any() ? latest.First().Id + 1 : 1);
        }        
        public int GetNewInformationId()
        {
            List<Information> latest = Informations.OrderByDescending(q => q.Id).ToList();

            return (latest.Any() ? latest.First().Id + 1 : 1);
        }
    }
}
