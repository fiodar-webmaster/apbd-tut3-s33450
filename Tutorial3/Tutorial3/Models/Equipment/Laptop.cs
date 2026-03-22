namespace Tutorial3.Models.Equipment;

public class Laptop : Equipment
{
    public ScreenResolution ScreenResolution { get; set; }
    public string Processor { get; set; }

    public Laptop(string name, ScreenResolution resolution, string processor) : base(name)
    {
        ScreenResolution = resolution;
        Processor = processor;
    }
}