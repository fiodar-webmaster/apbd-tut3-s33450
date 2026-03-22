namespace Tutorial3.Models.Equipment;

public class Camera : Equipment
{
    public int FocalLength { get; set; }
    public bool HasBluetooth { get; set; }

    public Camera(string name, int focalLength, bool hasBluetooth) : base(name)
    {
        FocalLength = focalLength;
        HasBluetooth = hasBluetooth;
    }
}