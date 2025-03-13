namespace teko_projektarbeit_loesungsalgorithmen.Objects
{
    public class Information
{
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public string Description { get; set; }
        public string[] Tags { get; set; }
        public int AuthorId {  get; set; }

        public int Version { get; set; }

        public Information(int id, int projectId, string description, int authorId, string[]? tags, int version)
        {
            Id = id;
            ProjectId = projectId;
            Description = description;
            AuthorId = authorId;
            Tags = tags ?? new string[3];
            Version = version;
        }
}
}
