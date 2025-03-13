namespace teko_projektarbeit_loesungsalgorithmen.Objects {
public class Project
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int ProjectLeadId { get; set; }

    public string CustomerName { get; set; }

    public string Description { get; set; }

    public int Version { get; set; }

    public Project(int id, string name, int projectLeadId, string customerName, string description, int version = 1)
    {
        Id = id;
        Name = name;
        ProjectLeadId = projectLeadId;
        CustomerName = customerName;
        Description = description;
        Version = version;
    }

}
}
