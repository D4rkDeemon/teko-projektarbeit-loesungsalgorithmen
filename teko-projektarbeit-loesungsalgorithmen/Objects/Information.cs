namespace teko_projektarbeit_loesungsalgorithmen.Objects
{
    public class Information
{
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public string Description { get; set; }
        public string[] Tags { get; set; }
        public User Author {  get; set; }

        public int Version { get; set; }

        public Information(string description, User author, string[]? tags, int version)
        {
            Description = description;
            Author = author;
            Tags = tags ?? new string[3];
            Version = version;
        }
}
}
