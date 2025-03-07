namespace teko_projektarbeit_loesungsalgorithmen.Objects {
public class Project
{

        public int Id { get; set; }
    public string Name { get; set; }

    public User ProjectLead { get; set; }

    public string CustomerName { get; set; }

    public string Description { get; set; }

    public Project(string name, User projectLead, string customerName, string description)
    {
        Name = name;
        ProjectLead = projectLead;
        CustomerName = customerName;
        Description = description;
    }

}
}
