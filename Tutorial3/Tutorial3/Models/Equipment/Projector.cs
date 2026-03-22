namespace Tutorial3.Models.Equipment;

public class Projector : Equipment
{
    public int Brightness { get; set; }
    public bool HasSpeakers { get; set; }

    public Projector(string name, int brightness, bool hasSpeakers) : base(name)
    {
        Brightness = brightness;
        HasSpeakers = hasSpeakers;
    }    
}