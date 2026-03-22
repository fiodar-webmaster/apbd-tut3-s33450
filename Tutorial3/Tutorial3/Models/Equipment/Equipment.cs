namespace Tutorial3.Models.Equipment;

public abstract class Equipment {
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; set; }
    public bool IsAvailable { get; set; } = true;

    protected Equipment(string name)
    {
        Name = name;
    }
     
}