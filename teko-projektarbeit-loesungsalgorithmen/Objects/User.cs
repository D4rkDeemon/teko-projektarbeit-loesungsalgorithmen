namespace teko_projektarbeit_loesungsalgorithmen.Objects
{
    public class User
{
        public int Id { get; set; }

        public enum ProjectRole
        {
            Lead,
            Staff
        }

        public string Name { get; set; }
        public ProjectRole Role { get; set; }

        public User(int id, string name, ProjectRole role)
        {
            Id = id;
            Name = name;
            Role = role;
        }
    }
}
