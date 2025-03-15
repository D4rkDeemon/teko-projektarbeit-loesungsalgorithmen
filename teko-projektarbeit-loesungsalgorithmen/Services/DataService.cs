using System.Collections.Generic;
using System.Text.Json;
using System.Xml.Linq;
using System.Xml.Serialization;
using teko_projektarbeit_loesungsalgorithmen.Objects;

namespace teko_projektarbeit_loesungsalgorithmen.Services
{
    // Handles the data storage and retrieval
    public class DataService
    {
        // The folder where the json files are stored
        const string StorageFolder = "Storage/";

        private List<Project> _projects;
        private List<User> _users;
        private List<Information> _informations;

        // Trigger a json update whenever the data is set, to keep the data in sync
        public List<Project> Projects
        {
            get => _projects;
            set
            {
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

        // Load the data from a json file based on the supplied type
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
            return new List<Project>() { 
                new Project(
                    1,
                    "Webauftritt teko.ch",
                    Users[0].Id,
                    "TEKO",
                    "Realisierung eines neuen Webauftritts für die TEKO Fachhoschlue."
                )
            };
        }
        private List<User> CreateDemoUsers()
        {
            return new List<User> { 
                new User(
                    1,
                    "John",
                    User.ProjectRole.Lead
                ), 
                new User(
                    2,
                    "Sam",
                    User.ProjectRole.Staff
                )
            };
        }

        // Write the data to a json file based on the supplied type
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

        // Get all prjects, optionally including all versions
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

            foreach (Information information in Informations)
            {

                if (information.ProjectId == id)
                {
                    resultList.Add(information);
                }
            }

            return resultList;
        }

        // Get the next free id for a new project
        public int GetNewProjectId()
        {
            List<Project> latest = Projects.OrderByDescending(q => q.Id).ToList();

            return (latest.Any() ? latest.First().Id + 1 : 1);
        }

        // Get the next free id for a new information
        public int GetNewInformationId()
        {
            List<Information> latest = Informations.OrderByDescending(q => q.Id).ToList();

            return (latest.Any() ? latest.First().Id + 1 : 1);
        }
    }
}
